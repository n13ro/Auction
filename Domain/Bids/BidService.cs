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
            bid.Cancel();
            return Task.CompletedTask;
        }

        public Task<bool> CanUserPlaceBidAsync(int userId, int lotId, long amount)
        {
            throw new NotImplementedException();
        }

        public Task<Bid> CreateBidAsync(int userId, int lotId, long amount)
        {
            var newBid = new Bid(userId, lotId, amount);
            return Task.FromResult(newBid);
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
