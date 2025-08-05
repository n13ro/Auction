using Domain.Bids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lots
{
    public class LotService : ILotService
    {
        public Task AddBidAsync(Lot lot, Bid bid)
        {
            if (!lot.IsActive)
            {
                throw new Exception("Lot is NOT active");
            }

            var winBid = GetWinningBid(lot);
            if (winBid.Amount >= bid.Amount)
            {
                throw new Exception("This bid > Last bid");
            }

            lot.AddBid(bid);

            return Task.CompletedTask;
        }

        public Task CloseLotAsync(Lot lot)
        {
            if (!lot.IsActive)
            {
                lot.CloseLot();
            }
            return Task.CompletedTask;
        }

        public Task CloseLotByUserAsync(Lot lot)
        {
            if (!lot.IsActive)
            {
                throw new Exception("This lot not active");
            }
            lot.CloseLotByUser();
            return Task.CompletedTask;
        }

        public Task<Lot> CreateLotAsync(string name, string description, 
            long startingPrice, long minBet, 
            bool isExtraTime, TimeSpan lotLife)
        {
            var newLot = new Lot(
                name, description, 
                startingPrice, minBet, 
                isExtraTime, lotLife);

            return Task.FromResult(newLot);
        }

        public Task ExtendTimeAsync(Lot lot)
        {
            if (lot.IsActive && lot.IsExtraTime)
            {
                lot.ExtendTime();
            }
            else
            {
                throw new Exception("Is IsExtraTime = false");
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Lot>> GetActiveLotsAsync()
        {

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bid>> GetBidsAsync(Lot lot)
        {
            throw new NotImplementedException();
        }

        public Task<Lot?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lot>> GetLotsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Bid GetWinningBid(Lot lot)
        {
            var winBid = lot.Bids.OrderByDescending(b => b.Id)
                .FirstOrDefault();

            if (winBid == null)
            {
                throw new Exception("Winning bid this null");
            }

            return winBid;
        }

        public Task<bool> IsActiveAsync(Lot lot)
        {
            return Task.FromResult(lot.IsActive);
        }
    }
}
