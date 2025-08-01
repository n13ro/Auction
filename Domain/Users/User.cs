using Domain.Bids;
using Domain.Common;
using Domain.Lots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public void Withdraw(long amount)
        {
            if(amount > 0 && amount <= Balance)
            Balance -= amount;
            SetUpdate();
        }

        public Lot CreateLot(string name,
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
            return newLot;
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
            if (lot.Status != Lot.LotStatus.Active)
            {
                return;
            }
            var winningBidAmount = lot.Bids.LastOrDefault().Amount; // The amount of winning bet
            if (!CheckBalance(winningBidAmount)
                || !(amount >= lot.MinBet)
                || !(amount > winningBidAmount))
            {
                throw new Exception("Not enough money for making a bid!");
            }
            Withdraw(amount);
            var bid = new Bid(Id, lot.Id, amount);
            lot.Bids.Add(bid);
            lot.ExtendTime();
            SetUpdate();
        }

        public void ProcessLotCompletion(Lot lot)
        {
            if (lot.Status != Lot.LotStatus.Closed || !lot.Bids.Any())
            {
                return;
            }
            var winningBid = lot.Bids
                .OrderByDescending(b => b.Amount).First();
            if (winningBid.UserId != Id)
            {
                var userBids = lot.Bids
                    .Where(b => b.UserId == Id).ToList();
                foreach(var bid in userBids)
                {
                    Deposit(bid.Amount);
                }
            }
            else
            {
                var userBids = lot.Bids
                    .Where(b => b.UserId == Id).ToList();
                foreach(var bid in userBids)
                {
                    if (bid.Amount != winningBid.Amount)
                    {
                        Deposit(bid.Amount);
                    }
                }
            }
        }

        private void ValidateData(
            string? nickName, 
            string? email, 
            string? password)
        {
            if(string.IsNullOrWhiteSpace(nickName) || nickName?.Length < 4 || nickName?.Length > 20 )
            {
                throw new Exception("Invalid data in user nickname");
            }
            if (nickName.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            {
                throw new ArgumentException("Nickname can only contain letters, digits, and underscores.", nameof(nickName));
            }
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Invalid data in user email");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Invalid password");
            }
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                throw new ArgumentException("Password must contain at least one uppercase letter, one lowercase letter, and one digit.", nameof(password));
            }
        }

    }
}