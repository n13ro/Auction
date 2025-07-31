using Domain.Bids;
using Domain.Common;
using Domain.Lots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class User : BaseEntity
    {
        [Range(4, 20, ErrorMessage = "Invalid NickName length")]
        public string NickName { get; private set; }
        [Range(4, 20, ErrorMessage = "Invalid Email length")]
        public string Email { get; private set; }
        public string Password { get; private set; }
        public long Balance { get; private set; }

        private List<Lot> _lots = new();
        public ICollection<Lot> Lots => _lots;

        private User() { }

        public User(string nickName, string email, string password)
        {
            ValidateData(nickName, email, password);
            NickName = nickName;
            Email = email;
            Password = password;
            Balance = 0;
            SetUpdate();
        }
        private void ValidateData(string? nickName, string? email, string? password)
        {
            if (string.IsNullOrWhiteSpace(nickName) && string.IsNullOrEmpty(nickName))
            {
                throw new Exception("Invalid data in nickName");
            }
            if (string.IsNullOrWhiteSpace(email) && email.Contains('@'))
            {
                throw new Exception("Invalid data in email");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Invalid data in password");
            }
        }
        public void UpdateNickName(string newNick)
        {
            if(newNick != NickName && 
                newNick.Length >= 4
                )
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
            if(amount > 0 && amount < 100_001)
            {
                Balance += amount;
                SetUpdate();
            }
        }


        public void CreateLot(
            string name,
            string description,
            long startingPrice,
            long minBet,
            bool isExtraTime,
            TimeSpan lotLife
            )
        {
            Lot newLot = new Lot(
                name,
                description,
                startingPrice,
                minBet,
                isExtraTime,
                TimeSpan.FromMinutes(lotLife.Minutes)
                );
            
            Lots.Add( newLot );
            SetUpdate();
        }
        public bool CheckBalanceBidOnLot(long amount)
        {
            return this.Balance > amount;
        }

        public void PlaceBidOnLot(Lot lot, long amount)
        {
            var winBidAmount = lot.Bids.LastOrDefault().Amount;
            //long winningBidAmount = winBid.Amount;
            if (!CheckBalanceBidOnLot(winBidAmount) 
                && amount >= lot.MinBet 
                && amount > winBidAmount)
            {
                throw new Exception("not money");
            }
            var bid = new Bid(Id, lot.Id, amount);
            lot.Bids.Add(bid);
            lot.ExtendTime();
            SetUpdate();

        }
        
        public void CloseLot(int id)
        {
            var thisLot = Lots.FirstOrDefault(k => k.Id == id);
            thisLot?.CloseLot();
            SetUpdate();
        }
    }
}
