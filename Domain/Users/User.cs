using Domain.Bids;
using Domain.Core;
using Domain.Lots;
using System.ComponentModel.DataAnnotations;


namespace Domain.Users
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : BaseEntity
    {
        [Range(4, 20, ErrorMessage = "Invalid NickName length")]
        [Required]
        public string NickName { get; private set; }

        [Range(4, 20, ErrorMessage = "Invalid Email length")]
        [Required]
        public string Email { get; private set; }

        [Range(4, 20, ErrorMessage = "Invalid Password length")]
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
                throw new Exception("Amount < 0 or > 100.000");
            }
        }

        public void ReturnMoney(long amount)
        {
            Balance += amount;
            SetUpdate();
        }

        public void Withdraw(long amount)
        {
            if (CheckBalance(amount))
            {
                Balance -= amount;
                SetUpdate();
            }
            else 
            {
                throw new Exception("Not enough money");
            }
        }

        public bool CheckBalance(long amount)
        {
            return Balance >= amount;
        }

        public void AddLot(Lot lot)
        {
            _lots.Add(lot);
            SetUpdate();
        }

        /// <summary>
        /// Лишнее?
        /// </summary>
        /// <param name="lotId"></param>
        /// <returns></returns>
        public Lot? GetLot(int lotId)
        {
            return _lots.FirstOrDefault(l => l.Id == lotId);
        }

        /// <summary>
        /// Метод для внешнего использования
        /// </summary>
        public void UpdateToLastModified()
        {
            SetUpdate();
        }

    }
}
