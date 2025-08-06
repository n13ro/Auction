using Domain.Bids;
using Domain.Lots;
using Domain.Users;
using Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositores.Bids
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _ctx;

        public BidRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Bid> CreateBidAsync(long amount)
        {
            var newBid = new Bid(amount);

            await _ctx.Bids.AddAsync(newBid);
            await _ctx.SaveChangesAsync();
            return newBid;
        }

        public async Task MarkAsLosingAsync(Bid bid)
        {
            bid.MarkAsLosing();
            await _ctx.SaveChangesAsync();
        }

        public async Task MarkAsWinningAsync(Bid bid)
        {
            bid.MarkAsWinning();
            await _ctx.SaveChangesAsync();
        }
    }
}
