using Domain.Bids;
using Domain.Common;
using Domain.Lots;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

using static Domain.Lots.Lot;


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
            if(amount > 0 && amount <= 100_000)
            {
                Balance += amount;
                SetUpdate();
            }
        }

        public void Withdraw(long amount)
        {
            if(amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                SetUpdate();
            }
        }

        public Lot CreateLot(
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
            return newLot;
        }
        public bool CheckBalanceBidOnLot(long amount)
        {
            return this.Balance >= amount;
        }

        public void PlaceBidOnLot(Lot lot, long amount)
        {
            var lastBid = lot.Bids.LastOrDefault();
            var lastBidAmount = lastBid?.Amount ?? lot.StartingPrice;

            if (!CheckBalanceBidOnLot(amount) || 
                amount < lot.MinBet || 
                amount <= lastBidAmount)
            {
                throw new Exception("not money");
            }

            Withdraw(amount);
            var bid = new Bid(Id, lot.Id, amount);
            lot.Bids.Add(bid);
            lot.ExtendTime();
            SetUpdate();
        }
        
        public void ProcessLotCompletion(Lot lot)
        {
            if (lot.Status != LotStatus.Closed ||
                !lot.Bids.Any())
            {
                return;
            }

            var winningBid = lot.Bids
                .OrderByDescending(b => b.Amount).First();
            
            if (winningBid.UserId != Id)
            {
                var userBids = lot.Bids
                    .Where(b => b.UserId == Id)
                    .ToList();

                foreach (var bid in userBids)
                {
                    Deposit(bid.Amount);
                }
            }
            
            if(winningBid.UserId == Id)
            {
                var userBids = lot.Bids
                    .Where(b => b.UserId == Id)
                    .ToList();

                foreach (var bid in userBids)
                {
                    if (bid.Amount != winningBid.Amount)
                    {
                        Deposit(bid.Amount);
                    }
                }
            }
        }

        public void CloseLot(int id)
        {
            var thisLot = Lots.FirstOrDefault(k => k.Id == id);
            if (thisLot != null)
            {
                thisLot?.CloseLot();
                ProcessLotCompletion(thisLot);
                
            }
            SetUpdate();
        }
    }
}
