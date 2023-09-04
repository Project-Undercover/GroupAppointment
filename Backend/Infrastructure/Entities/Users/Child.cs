using Infrastructure.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Users
{
    public class Child : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public User User { get; set; }
    }
}
