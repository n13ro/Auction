using Domain.Bids;
using Domain.Lots;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositores.Bids
{
    public interface IBidRepository
    {
        Task<Bid> CreateBidAsync(long amount);
        Task MarkAsWinningAsync(Bid bid);
        Task MarkAsLosingAsync(Bid bid);
    }
}
