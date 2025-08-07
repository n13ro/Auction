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
        [Range(4, 20, ErrorMessage = "Invalid NickName length")]
        public string NickName { get; private set; }
        [Range(4, 20, ErrorMessage = "Invalid Email length")]
        public string Email { get; private set; }
        public string Password { get; private set; }
        public long Balance { get; private set; }

        private List<Lot> _lots = new();
        public ICollection<Lot> Lots => _lots;

        public int LotId { get; private set; }
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
            Balance += amount;
            SetUpdate();
        }

        public void ReturnMoney(long amount)
        {
            Balance += amount;
            SetUpdate();
        }

        public void Withdraw(long amount)
        {
            Balance -= amount;
            SetUpdate();
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