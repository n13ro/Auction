using System.ComponentModel.DataAnnotations;


namespace Domain.Core
{
    /// <summary>
    /// Базовая модель для наследования
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; private set; }
        public DateTime CreateAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        protected BaseEntity()
        {
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
        /// <summary>
        /// Обновление данных при любых изменениях
        /// </summary>
        protected void SetUpdate()
        {
            UpdateAt = DateTime.UtcNow;
        }

    }
}
