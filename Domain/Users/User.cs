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
