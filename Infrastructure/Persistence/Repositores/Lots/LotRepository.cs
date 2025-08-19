using Domain.Lots;
using Domain.Users;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositores.Lots
{
    /// <summary>
    /// Интерфейс методов действий ЛОТА
    /// </summary>
    public interface ILotRepository
    {
        /// <summary>
        /// Запросы к лоту формата GET
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Lot>> GetAllAsync();
        Task<Lot> GetByIdLot(int lotId);
        /// <summary>
        /// Запросы к лоту формата POST
        /// </summary>
        Task<Lot> CreateLotAsync(int userId, string name,
            string description, long startingPrice,
           long minBet, bool isExtraTime, double lotLife);
        Task CloseLotAsync(Lot lot);
        Task CloseLotByUserAsync(Lot lot);
    }

    /// <summary>
    /// Реализация метовов интерфейса действий ЛОТА
    /// </summary>
    public class LotRepository : ILotRepository
    {
        /// <summary>
        /// Инициализация контекста
        /// </summary>
        private readonly AppDbContext _ctx;

        public LotRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CloseLotAsync(Lot lot)
        {
            if (!lot.IsActive)
            {
                lot.CloseLot();
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task CloseLotByUserAsync(Lot lot)
        {
            if (!lot.IsActive)
            {
                lot.CloseLotByUser();
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<Lot> CreateLotAsync(int userId, string name, 
            string description, long startingPrice, 
            long minBet, bool isExtraTime, double lotLife)
        {
            var user = await _ctx.Users.FindAsync(userId);
            
            Lot newLot = new(name, description,
                                startingPrice, minBet,
                                isExtraTime, lotLife);
            user?.AddLot(newLot);
            user?.UpdateToLastModified();
            await _ctx.Lots.AddAsync(newLot);
            await _ctx.SaveChangesAsync();
            return newLot;
        }

        public async Task<IEnumerable<Lot>> GetAllAsync()
        {
            return await _ctx.Lots.ToListAsync();
        }

        public Task<Lot> GetByIdLot(int lotId)
        {
            return _ctx.Lots.FirstAsync(k => k.Id == lotId);
        }
    }
}
