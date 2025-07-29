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
        public Lot CurrentLot { get; private set; }
        public User User { get; private set; }
        public long Amount { get; private set; }
        public DateTime PlacedAt { get; private set; }

        private Bid() { }
        public Bid(User user, long amount, Lot currentLot)
        {
            if (amount >= currentLot.MinBet) Amount = amount;
            else throw new Exception("Amount <=");

            int Id = user.Id;
            User = user;
            CurrentLot = currentLot;
            PlacedAt = DateTime.Now;
        }


    }
}
