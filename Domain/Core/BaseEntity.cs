using System.ComponentModel.DataAnnotations;


namespace Domain.Core
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; private set; }
        public DateTime CreateAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        protected BaseEntity()
        {
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        protected void SetUpdate()
        {
            UpdateAt = DateTime.Now;
        }

    }
}
