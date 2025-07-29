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
        public long Amount { get; }
        public User User { get; }
        public DateTime PlacedAt { get; }

        public Bid(long amount, User user, Lot currentLot, DateTime timestamp)
        {
            if (amount is 0) throw new ArgumentNullException(nameof(amount));
            if (user is null) throw new ArgumentNullException(nameof(user));

            CurrentLot = currentLot;
            if (amount >= currentLot.MinBet) Amount = amount;
            else throw new Exception("You trying to place a bid, that is lower than minimum!");
            User = user;
            PlacedAt = timestamp;
            int ID = user.Id;
        }

   
    }
}
