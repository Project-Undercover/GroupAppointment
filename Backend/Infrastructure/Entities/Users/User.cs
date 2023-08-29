using Infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities.Users
{
    [PrimaryKey(nameof(Id))]
    [Index(nameof(Email), IsUnique = true), Index(nameof(MobileNumber), IsUnique = true)]
    public class User : Entity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
