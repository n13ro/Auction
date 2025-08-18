using Domain.Bids;
using Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Domain.Lots
{
    /// <summary>
    /// Модель лота
    /// </summary>
    public class Lot : BaseEntity
    {
        [Range(4, 20, ErrorMessage = "Invalid NameLot length")]
        [Required]
        public string Name { get; private set; }

        [Range(4, 20, ErrorMessage = "Invalid Description Lot length")]
        [Required]
        public string Description { get; private set; }

        [Range(1000, 100_000_000_000_000, ErrorMessage = "Invalid Starting price ")]
        [Required]
        public long StartingPrice { get; private set; }

        [Range(100, 100_000_000, ErrorMessage = "Invalid Min bet price ")]
        [Required]
        public long MinBet { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// будет ли добавляться +2мин. после новой ставки?
        /// IsExtraTime: true
        /// </summary>
        [Required]
        public bool IsExtraTime { get; private set; }
        public LotStatus Status { get; private set; } = LotStatus.Active;


        private readonly List<Bid> _bids = new();
        public ICollection<Bid> Bids => _bids;

        private Lot() { }

        public Lot(string name, string description, long startingPrice, 
            long minBet,bool isExtraTime,double lotLife)
        {
            ValidateLotData(name,description,startingPrice,minBet,lotLife);
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            MinBet = minBet;
            StartTime = DateTime.UtcNow;
            EndTime = StartTime.AddMinutes(lotLife);
            IsExtraTime = isExtraTime;
            SetUpdate();
        }

        private void ValidateLotData(string name, string description, long startingPrice, long minBet, double lotLife)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Название лота не может быть пустым");

            if (name.Length < 4 || name.Length > 20)
                throw new ValidationException("Название лота должно быть от 4 до 20 символов");

            if (string.IsNullOrWhiteSpace(description))
                throw new ValidationException("Описание лота не может быть пустым");

            if (description.Length < 4 || description.Length > 20)
                throw new ValidationException("Описание лота должно быть от 4 до 20 символов");

            if (startingPrice < 1000)
                throw new ValidationException("Начальная цена должна быть не менее 1000");

            if (minBet < 100)
                throw new ValidationException("Минимальная ставка должна быть не менее 100");

            if (minBet > startingPrice)
                throw new ValidationException("Минимальная ставка не может быть больше начальной цены");

            if (lotLife <= 0)
                throw new ValidationException("Время жизни лота должно быть положительным");

            if (lotLife > 1440) // 24 часа в минутах
                throw new ValidationException("Время жизни лота не может превышать 24 часа");
        }

        /// <summary>
        /// Активное ли время или нет?
        /// </summary>
        public bool IsActive =>
            Status == LotStatus.Active &&
            DateTime.UtcNow >= StartTime &&
            DateTime.UtcNow <= EndTime;

        /// <summary>
        /// Добавляем две минуты если стоит true
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void ExtendTime()
        {
            if (IsExtraTime)
            {
                EndTime = EndTime.Add(TimeSpan.FromMinutes(2));
                SetUpdate();
            }
            else
            {
                throw new InvalidOperationException("Invalid adding time operation");
            }
        }

        /// <summary>
        /// Отменяем/закрываем лот руками
        /// </summary>
        public void CloseLotByUser()
        {
            if (IsActive)
            {
                Status = LotStatus.ClosedByUser;
                SetUpdate();
            }
        }

        /// <summary>
        /// Закрываем лот по завершению времени
        /// </summary>
        public void CloseLot()
        {
            if (!IsActive)
            {
                Status = LotStatus.Closed;
                SetUpdate();
            }

        }

        /// <summary>
        /// Добавление ставки в конкретный лот при успехе
        /// </summary>
        /// <param name="bid"></param>
        public void AddBid(Bid bid)
        {
            _bids.Add( bid );
        }

        public enum LotStatus : byte
        {
            Active,
            Closed,
            ClosedByUser

        }
    }
}
