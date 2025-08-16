using Domain.Bids;
using Domain.Core;
using Domain.Lots;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

using static Domain.Lots.Lot;


namespace Domain.Users
{
    public class User : BaseEntity
    {
        [Required]
        [Range(4, 20, ErrorMessage = "Invalid NickName length")]
        public string NickName { get; private set; }
        [Required]
        [Range(4, 20, ErrorMessage = "Invalid Email length")]
        public string Email { get; private set; }
        [Required]
        public string Password { get; private set; }
        public long Balance { get; private set; }

        private readonly List<Lot> _lots = new();
        private readonly List<Bid> _bids = new();
        public ICollection<Lot> Lots => _lots;
        public ICollection<Bid> Bids => _bids;


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
            NickName = newNick;
            SetUpdate();
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
            SetUpdate();
        }

        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
            SetUpdate();
        }

        public void Deposit(long amount)
        {
            if (amount > 0 && amount <= 100_000)
            {
                Balance += amount;
                SetUpdate();
            }
            else
            {
                throw new Exception("Wrong amount of money!");
            }
        }

        public void ReturnMoney(long amount)
        {
            Balance += amount;
            SetUpdate();
        }

        public void Withdraw(long amount)
        {
            if (CheckBalanceBidOnLot(amount))
            {
                Balance -= amount;
                SetUpdate();
            }
            else
            {
                throw new Exception("Not enough money on balance!");
            }
        }

        public bool CheckBalanceBidOnLot(long amount)
        {
            return Balance >= amount;
        }

        public void AddLot(Lot lot)
        {
            _lots.Add(lot);
            SetUpdate();
        }

        public Lot? GetLot(int lotId)
        {
            return _lots.FirstOrDefault(l => l.Id == lotId);
        }

        public void UpdateToLastModified()
        {
            SetUpdate();
        }

    }
}
