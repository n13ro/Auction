using Domain.Bids;
using Domain.Lots;
using Domain.Users;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositores.Users.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<User> GetByIdUserAsync(int id)
        {
            var byUser = await _ctx.Users.FindAsync(id);

            return byUser;
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
        public async Task UpdateUserDataAsync(UpdateUserDataRequest req)
        {
            await _ctx.Users
                .Where(u => u.Id == req.Id)
                .ExecuteUpdateAsync(user => user
                    .SetProperty(u => u.NickName, req.NickName)
                    .SetProperty(u => u.Email, req.Email)
                    .SetProperty(u => u.Password, req.Password)
                    .SetProperty(u => u.UpdateAt, DateTime.UtcNow)
                );
            await _ctx.SaveChangesAsync();

        }


        public async Task<bool> CanUserBidOnLotAsync(User user, Lot lot, long amount)
        {
            return user.Balance >= amount && lot.IsActive;
        }


        public async Task<User> GetByEmailUserAsync(string email)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task DepositOnBalanceAsync(int id, long amount)
        {
            var user = await _ctx.Users.FindAsync(id);
            user?.Deposit(amount);
            await _ctx.SaveChangesAsync();
        }

        public async Task PlaceBidAsync(int userId, int lotId, long amount)
        {
            //user by id
            var user = await _ctx.Users.FindAsync(userId);

            //all lots and all bids in this lot
            var lot = await _ctx.Lots
                .Include(lot => lot.Bids)
                .SingleOrDefaultAsync(k => k.Id == lotId);
            //last bid by amount
            var last = await _ctx.Bids
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            if( user != null && lot != null)
            {
                if(amount <= user.Balance && (last?.Amount == null || amount > last.Amount))
                {
                    //user.Withdraw(amount);
                    var nextBid = new Bid(amount);
                    nextBid.SetUserId(userId);
                    nextBid.SetLotId(lotId);
                    nextBid.MarkAsWinning();

                    lot?.AddBid(nextBid);
                    lot?.ExtendTime();


                    await _ctx.Bids.AddAsync(nextBid);
                    await _ctx.SaveChangesAsync();

                }

            }



        }

        public Task<bool> CanUserBidOnLotAsync(int userId, int lotId, long amount)
        {
            throw new NotImplementedException();
        }

        public Task WithdrawWonBidsAsync(int userId, int lotId)
        {
            throw new NotImplementedException();
        }
    }
}
