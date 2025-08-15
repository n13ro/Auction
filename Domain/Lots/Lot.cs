using Domain.Bids;
using Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Domain.Lots
{
    public class Lot : BaseEntity
    {
        [Range(4, 20, ErrorMessage = "Invalid NameLot length")]
        [Required]
        public string Name { get; private set; }

        [Range(4, 20, ErrorMessage = "Invalid Description Lot length")]
        [Required]
        public string Description { get; private set; }

        [Range(1000, 100_000_000_000_000, ErrorMessage = "Invalid Starting price ")]
        [Required]
        public long StartingPrice { get; private set; }

        [Range(100, 100_000_000, ErrorMessage = "Invalid Min bet price ")]
        [Required]
        public long MinBet { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        [Required]
        public bool IsExtraTime { get; private set; }
        public LotStatus Status { get; private set; } = LotStatus.Active;


        private readonly List<Bid> _bids = new();
        public ICollection<Bid> Bids => _bids;

        private Lot() { }

        public Lot(string name, string description, long startingPrice, 
            long minBet,bool isExtraTime,double lotLife)
        {
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            MinBet = minBet;
            StartTime = DateTime.UtcNow;
            EndTime = StartTime.AddMinutes(lotLife);
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
                SetUpdate();
            }
            else
            {
                throw new InvalidOperationException("Invalid adding time operation");
            }
        }

        public void CloseLotByUser()
        {
            if (IsActive)
            {
                Status = LotStatus.ClosedByUser;
                SetUpdate();
            }
        }

        public void CloseLot()
        {
            if (!IsActive)
            {
                Status = LotStatus.Closed;
                SetUpdate();
            }

        }

        public void AddBid(Bid bid)
        {
            _bids.Add( bid );
        }

        public enum LotStatus : byte
        {
            Active,
            Closed,
            ClosedByUser

        }
    }
}
