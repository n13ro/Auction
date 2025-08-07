using Domain.Core;
using Domain.Lots;
using Domain.Users;

namespace Domain.Bids
{
    public class Bid : BaseEntity
    {
        public long Amount { get; private set; }
        public DateTime PlacedAt { get; private set; }
        public BidStatus Status { get; private set; }


        private Bid() { }
        public Bid(long amount)
        {
            Amount = amount;
            PlacedAt = DateTime.UtcNow;
            SetUpdate();
        }

        public void MarkAsWinning()
        {
            Status = BidStatus.Winning;
            SetUpdate();
        }

        public void MarkAsLosing()
        {
            Status = BidStatus.Losing;
            SetUpdate();
        }
        public void Cancel()
        {
            Status = BidStatus.Cancelled;
            SetUpdate();
        }


        public enum BidStatus
        {
            Active,
            Cancelled,
            Winning,
            Losing
        }
    }
}
