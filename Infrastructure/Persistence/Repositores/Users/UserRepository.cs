using Domain.Bids;
using Domain.Users;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositores.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositores.Users
{
    /// <summary>
    /// Интерфейс методов действий ПОЛЬЗОВАТЕЛЯ
    /// </summary>
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task UpdateUserDataAsync(UpdateUserDataRequest req);
        Task<User> GetByIdUserAsync(int id);
        Task<User> GetByEmailUserAsync(string email);
        Task PlaceBidAsync(int userId, int lotId, long amount);
        Task DepositOnBalanceAsync(int id, long amount);
        Task CloseLotAsync(int userId, int lotId);

        Task<User> GetByRefreshTokenAsync(string rt);

        Task SetRefreshTokenAsync(int userId, string refreshToken, DateTime life);
        Task RevorkeRefreshTokenAsync(int userId);

    }


    /// <summary>
    /// Реализация метовов интерфейса действий ПОЛЬЗОВАТЕЛЯ
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Инициализация контекста
        /// </summary>
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


        public async Task<User> GetByEmailUserAsync(string email)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task DepositOnBalanceAsync(int id, long amount)
        {
            try
            {
                var user = await _ctx.Users.FindAsync(id);
                if(user != null && (amount > 0 && amount <= 100.000))
                {
                    user.Deposit(amount);
                    await _ctx.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Error, user is null");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Error {ex}");
            }
        }

        /// <summary>
        /// Создание ставки как пользователь(кроме создателя)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lotId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                .Where(b => b.LotId == lotId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            //get id owner lot
            var ownerLot = await _ctx.Users
                .Where(k => k.Id == userId)
                .AnyAsync(k => k.Lots.Any(k=> k.Id == lotId));

            if( user != null && lot != null && !ownerLot)
            {
                if(amount <= user.Balance && (last?.Amount == null || amount > last.Amount))
                {
                    var nextBid = new Bid(amount);
                    nextBid.SetUserId(userId);
                    nextBid.SetLotId(lotId);
                    nextBid.MarkAsWinning();

                    lot?.AddBid(nextBid);
                    lot?.ExtendTime();


                    await _ctx.Bids.AddAsync(nextBid);
                    await _ctx.SaveChangesAsync();

                }
                else
                {
                    throw new Exception("Error amount");
                }

            }
            else
            {
                throw new Exception("Your owner or null");
            }



        }

        /// <summary>
        /// Закрытие лота руками как пользователь
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lotId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CloseLotAsync(int userId, int lotId)
        {
            try
            {
                var ownerLot = await _ctx.Users
                    .Where(k => k.Id == userId)
                    .AnyAsync(k => k.Lots.Any(k => k.Id == lotId));

                var thisLot = await _ctx.Lots.FirstOrDefaultAsync(k=> k.Id == lotId);

                if (ownerLot && thisLot != null)
                {
                    thisLot.CloseLotByUser();
                    await _ctx.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Null lot");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Operation fail");
            }
        }

        public async Task SetRefreshTokenAsync(int userId, string refreshToken, DateTime life)
        {
            var user = await _ctx.Users.FindAsync(userId);
            if(user != null)
            {
                user.SetRefreshToken(refreshToken, life);
                await _ctx.SaveChangesAsync();
            }
            else
            {
                throw new InvalidDataException("Error enter data");
            }
        }

        public async Task RevorkeRefreshTokenAsync(int userId)
        {
            var user = await _ctx.Users.FindAsync(userId);
            if (user == null) return;
            user.RevorkeRefreshToken();
            await _ctx.SaveChangesAsync();
        }

        public async Task<User?> GetByRefreshTokenAsync(string rt)
        {
            return await _ctx.Users.FirstOrDefaultAsync(x => x.RefreshToken == rt);
                
        }
    }
}
