using Domain.Bids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Lots
{
    public class LotService : ILotService
    {
        public Task AddBidAsync(Lot lot, Bid bid)
        {
            if (!lot.IsActive)
                throw new Exception("HUY");

            var winBid = GetWinningBid(lot);

            if (winBid.Amount >= bid.Amount 
                || bid.Amount < lot.MinBet
                || bid.Amount <= 0)
                throw new Exception("HUY");

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
                throw new Exception();
            }

            lot.CloseLotByUser();
            return Task.CompletedTask;
        }

        public Task<Lot> CreateLotAsync(string name, string description, 
            long startingPrice, long minBet, 
            bool isExtraTime, TimeSpan lotLife)
        {
            var newLot = new Lot(name, description, 
                startingPrice, minBet, 
                isExtraTime, lotLife);

            return Task.FromResult(newLot);
        }

        public Task ExtendTimeAsync(Lot lot)
        {
            if(lot.IsActive && lot.IsExtraTime)
            {
                lot.ExtendTime();
            }
            else
            {
                throw new Exception();
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Lot>> GetActiveLotsAsync()
        {
            throw new Exception();
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
            var winningBid = lot.Bids
                .OrderByDescending(b => b.Id).FirstOrDefault();

            if (winningBid == null)
                throw new Exception("H");
            
            return winningBid;
        }

        public Task<bool> IsActiveAsync(Lot lot)
        {
            return Task.FromResult(lot.IsActive);
        }
    }
}
