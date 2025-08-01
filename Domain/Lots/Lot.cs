using Domain.Bids;
using Domain.Common;
using Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Domain.Lots
{
    public class Lot : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public long StartingPrice { get; private set; }
        public long MinBet { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsExtraTime { get; private set; }
        public LotStatus Status { get; private set; } = LotStatus.Active;
        //public int CurrentBids { get; private set; }
        private List<Bid> _bids = new List<Bid>();
        public ICollection<Bid> Bids => _bids;

        private Lot() { }

        public Lot(
            string name, 
            string description, 
            long startingPrice, 
            long minBet,
            bool isExtraTime,
            TimeSpan lotLife  
            )
        {
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            MinBet = minBet;
            StartTime = DateTime.Now;
            EndTime = StartTime.Add(lotLife);
            IsExtraTime = isExtraTime;
            SetUpdate();
        }
        
        //time?
        public bool IsActive =>
            Status == LotStatus.Active &&
            DateTime.UtcNow >= StartTime &&
            DateTime.UtcNow <= EndTime;
        //

        public void ExtendTime()
        {
            if (IsExtraTime)
            {
                EndTime = EndTime.Add(TimeSpan.FromMinutes(2));
            }
        }

        public void CloseLot()
        {
            if(!IsActive)
            Status = LotStatus.Closed;

        }

        public enum LotStatus
        {
            Active,
            Closed,
            ClosedByUser
        }

        public void ReturnBids()
        {
            
        }
        
    }
}
