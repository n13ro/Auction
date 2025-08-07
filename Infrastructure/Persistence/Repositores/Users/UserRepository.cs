using Domain.Bids;
using Domain.Lots;
using Domain.Users;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositores.Users.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Lots.Lot;

namespace Infrastructure.Persistence.Repositores.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> CanUserBidOnLotAsync(User user, Lot lot, long amount)
        {
            return user.Balance >= amount && lot.IsActive;
        }

        public async Task CreateUserAsync(User user)
        {
            if (user != null)
            {
                var newUser = new User(
                    user.NickName,
                    user.Email,
                    user.Password);
                await _ctx.Users.AddAsync(newUser);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task UpdateUserDataAsync(UpdateUserDataRequest request)
        {
            var byUser = await _ctx.Users.FindAsync(request.Id);

            await _ctx.Users
                .Where(u => u.Id == request.Id)
                .ExecuteUpdateAsync(user => user
                    .SetProperty(u => u.NickName, request.NickName)
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Password, request.Password)
                    .SetProperty(u => u.UpdateAt, DateTime.UtcNow)
                );
            await _ctx.SaveChangesAsync();

        }

        public async Task<UserResponse> GetByIdUserAsync(int id)
        {
            var byUser = await _ctx.Users.FindAsync(id);

            return new UserResponse
            {
                Id = id,
                NickName = byUser.NickName,
                Email = byUser.Email,
                Password = byUser.Password,
                Balance = byUser.Balance
            };
        }

        public async Task PlaceBidAsync(User user, Lot lot, long amount)
        {
            if (!await CanUserBidOnLotAsync(user, lot, amount))
            {
                throw new Exception("Cannot place bid");
            }
            user.Withdraw(amount);

            var bid = new Bid(amount);
            
            lot.Bids.Add(bid);
            lot.ExtendTime();

            await _ctx.Bids.AddAsync(bid);

            user.UpdateToLastModified();
            await _ctx.SaveChangesAsync();
        }


        public async Task WithdrawWonBidsAsync(User user, Lot lot)
        {
            if(lot.Status != Lot.LotStatus.Active && lot.Bids.Any())
            {
                var winBid = lot.Bids.OrderByDescending(k => k.Amount)
                    .FirstOrDefault();

                user.Withdraw(winBid.Amount);
                await _ctx.SaveChangesAsync();
            }

        }
    }
}
