using Domain.Common;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lots
{
    public class Lot : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public long StartingPrice { get; private set; }
        public long CurrentPrice { get; private set; }
        public long MinBet { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsExtraTime { get; private set; }
        public LotStatus Status { get; private set; } = LotStatus.Active;

        private Lot() { }

        public Lot(
            string name, 
            string description, 
            long startingPrice,
            long minBet, 
            bool isExtraTime,
            TimeSpan LotLife
            )
        {
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            MinBet = minBet;
            StartTime = DateTime.Now;
            EndTime = StartTime.Add(LotLife);
            IsExtraTime = isExtraTime;
        }

        public bool IsActive => 
            Status == LotStatus.Active &&
            DateTime.UtcNow >= StartTime &&
            DateTime.UtcNow <= EndTime;

        public void Closeot()
        {
            Status = LotStatus.Closed;
        }
        

        public enum LotStatus
        {
            Active,
            Closed
        }
    }
}