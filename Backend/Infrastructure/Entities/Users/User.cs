using Infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Enums.Enums;

namespace Infrastructure.Entities.Users
{
    [PrimaryKey(nameof(Id))]
    [Index(nameof(Email), IsUnique = true), Index(nameof(MobileNumber), IsUnique = true)]
    public class User : Entity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string MobileNumber { get; set; } = null!;
        public int ChildrenNumber { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public UserRole Role { get; set; }
        public List<Child> Children { get; set; } = new List<Child>();
    }
}
