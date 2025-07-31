using Domain.Common;
using Domain.Bids;
using Domain.Lots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Users
{
    public class User : BaseEntity
    {
        [Range(4, 20, ErrorMessage = "Invalid nickname length")]
        public string NickName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public long Balance { get; private set; }
        private List<Lot> _lots = new List<Lot>();
        public ICollection<Lot> Lots => _lots;
        private User() { }

        public User(string nickName, string email, string password)
        {
            NickName = nickName;
            Email = email;
            Password = password;
            Balance = 0;
            SetUpdate();
        }

        public void UpdateNickName(string newNick)
        {
            if (newNick != NickName && newNick.Length >= 4)
            {
                NickName = newNick;
                SetUpdate();
            }
        }

        public void UpdateEmail(string newEmail)
        {
            if (newEmail.Contains('@'))
            {
                Email = newEmail;
                SetUpdate();
            }
        }

        public void UpdatePassword(string newPassword)
        {
            if (newPassword.Length >= 6)
            {
                Password = newPassword;
                SetUpdate();
            }
        }

        public void Deposit(long amount)
        {
            if (amount > 0 && amount < 100001)
            {
                Balance += amount;
                SetUpdate();
            }
        }



        public void CreateLot(string name,
            string description,
            long startingPrice,
            long minBet,
            bool isExtraTime,
            TimeSpan lotLife)
        {
            Lot newLot = new Lot(
                name, 
                description, 
                startingPrice, 
                minBet, 
                isExtraTime, 
                TimeSpan.FromMinutes(lotLife.Minutes));

            Lots.Add(newLot);
            SetUpdate();
        }

        public void CloseLot(int id)
        {
            var thisLot = Lots.Where(k => k.Id == id && k.IsActive).FirstOrDefault();
            thisLot?.CloseLot();
            SetUpdate();
        }

        public bool CheckBalance(long amount)
        {
            return this.Balance > amount;
        }

        public void PlaceBidOnLot(Lot lot, long amount)
        {
            var winningBidAmount = lot.Bids.LastOrDefault().Amount;
            if (!CheckBalance(winningBidAmount) 
                && amount >= lot.MinBet 
                && amount > winningBidAmount)
            {
                throw new Exception("Not enough money for making a bid!");
            }
            var bid = new Bid(Id, lot.Id, amount);
            lot.Bids.Add(bid);
            lot.ExtendTime();
            SetUpdate();
        }

        private void ValidateData(
            string? nickName, 
            string? email, 
            string? password)
        {
            if(string.IsNullOrWhiteSpace(nickName) && nickName?.Length >= 4)
            {
                throw new Exception("Invalid data in user nickname");
            }
            if (string.IsNullOrWhiteSpace(email) && )
            {
                throw new Exception("Invalid data in user email");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Invalid password");
            }
        }

    }
}
