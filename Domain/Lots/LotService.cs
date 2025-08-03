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
            throw new NotImplementedException();
        }

        public Task CloseLotAsync(Lot lot)
        {
            throw new NotImplementedException();
        }

        public Task CloseLotByUserAsync(Lot lot)
        {
            throw new NotImplementedException();
        }

        public Task<Lot> CreateLotAsync(string name, string description, long startingPrice, long minBet, bool isExtraTime, TimeSpan lotLife)
        {
            throw new NotImplementedException();
        }

        public Task ExtendTimeAsync(Lot lot)
        {
            throw new NotImplementedException();
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

        public Task<Bid?> GetWinningBidAsync(Lot lot)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsActiveAsync(Lot lot)
        {
            throw new NotImplementedException();
        }
    }
}
