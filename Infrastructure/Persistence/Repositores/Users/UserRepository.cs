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

            return await _ctx.Users.FindAsync(id);
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
            await using var transaction = await _ctx.Database.BeginTransactionAsync();
            try
            {
                var user = await _ctx.Users.FindAsync(userId);
                var lot = await _ctx.Lots
                    .Include(lot => lot.Bids)
                    .SingleOrDefaultAsync(k => k.Id == lotId);

                //last bid by amount
                var last = await _ctx.Bids
                    .Where(b => b.lotId == lotId)
                    .OrderByDescending(b => b.Amount)
                    .FirstOrDefaultAsync();

                var ownerLot = await _ctx.Users
                    .Where(k => k.Id == userId)
                    .AnyAsync(k => k.Lots.Any(k => k.Id == lotId));


                if (user != null && lot != null && !ownerLot)
                {                      
                    if (amount > 0 && amount <= user.Balance && amount > last?.Amount)
                    {
                        var nextBid = new Bid(amount);
                        nextBid.SetUserId(userId);
                        nextBid.SetLotId(lotId);
                        nextBid.MarkAsWinning();
                        lot?.AddBid(nextBid);
                        lot?.ExtendTime();
                        await _ctx.Bids.AddAsync(nextBid);
                        await _ctx.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        throw new Exception("Wrong amount!");
                    }
                }
                else
                {
                    throw new Exception("Invalid data!");
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new Exception("Error trying place bid on lot");
            }

            //all lots and all bids in this lot

            //user?.Withdraw(amount);

        }

        //public Task<bool> CanUserBidOnLotAsync(int userId, int lotId, long amount)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task CloseLotAsync(int userId, int lotId)
        {
            try
            {
                var lot = await _ctx.Lots
                    .Include(lot => lot.Bids)
                    .SingleOrDefaultAsync(k => k.Id == lotId);

                var ownerLot = await _ctx.Users
                    .Where(k => k.Id == userId)
                    .AnyAsync(k => k.Lots.Any(k => k.Id == lotId));

                if (lot != null && ownerLot)
                {
                    lot.CloseLotByUser();
                }
                else
                {
                    throw new Exception("Error trying closing lot!");
                }
            }
            catch
            {
                throw new Exception("Operation failed!");
            }
        }
    }
}
