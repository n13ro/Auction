using Domain.Common;
using Domain.Lots;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bids
{
    public class Bid : BaseEntity
    {
        public int UserId { get; private set; }
        public int LotId { get;private set; }
        public long Amount { get; private set; }
        public DateTime PlacedAt { get; private set; }
        public BidStatus Status { get; private set; }


        private Bid() { }
        public Bid(int userId, int lotId, long amount)
        {
            userId = UserId;
            lotId = LotId;
            amount = Amount;
            PlacedAt = DateTime.UtcNow;
            SetUpdate();
        }

        public enum BidStatus
        {
            Winnig,
            Losing
        }
    }
}
