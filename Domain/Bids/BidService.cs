using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bids
{
    internal class BidService : IBidService
    {
        public Task CancelBidAsync(Bid bid) 
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanUserPlaceBidAsync(int userId, int lotId, long amount)
        {
            throw new NotImplementedException();
        }

        public Task<Bid> CreateBidAsync(int userId, int lotId, 
            long amount)
        {
            throw new NotImplementedException();
        }

        public Task<Bid?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Bid?> GetHighestBidAsync(IEnumerable<Bid> bids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bid>> GetLotBidsAsync(int lotId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bid>> GetUserBidsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidBidAmountAsync(long amount, long minimumRequired)
        {
            throw new NotImplementedException();
        }

        public Task MarkAsLosingAsync(Bid bid)
        {
            throw new NotImplementedException();
        }

        public Task MarkAsWinningAsync(Bid bid)
        {
            throw new NotImplementedException();
        }
    }
}
