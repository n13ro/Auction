using Domain.Core;
using Domain.Lots;
using Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Domain.Bids
{
    public class Bid : BaseEntity
    {
        [Required]
        public long Amount { get; private set; }
        public DateTime PlacedAt { get; private set; }
        public BidStatus Status { get; private set; }
        public int userId { get; private set; }
        public User user { get; private set; }
        public int lotId { get; private set; }
        public Lot lot { get; private set; }


        private Bid() { }
        public Bid(long amount)
        {
            Amount = amount;
            PlacedAt = DateTime.UtcNow;
            SetUpdate();
        }

        public void SetUserId(int id)
        {
            userId = id;
            SetUpdate();
        }
        public void SetLotId(int id)
        {
            lotId = id;
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
