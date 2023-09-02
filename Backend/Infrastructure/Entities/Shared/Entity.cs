using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities.Shared
{
    [Index(nameof(Id), IsUnique = true)]
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected Entity(DateTime? createdAt = null)
        {
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateDate(DateTime date)
        {
            UpdatedAt = date;
        }
    }
}
