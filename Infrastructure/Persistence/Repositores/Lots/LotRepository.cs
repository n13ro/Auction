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
    public class LotRepository : ILotRepository
    {
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
                //if (user != null)
            {
                Lot newLot = new(name, description,
                                startingPrice, minBet,
                                isExtraTime, lotLife);

                user?.AddLot(newLot);
                user?.UpdateToLastModified();

                await _ctx.Lots.AddAsync(newLot);
                await _ctx.SaveChangesAsync();

                return newLot;
                //}
                //return null;
            }
        }
    }
}
