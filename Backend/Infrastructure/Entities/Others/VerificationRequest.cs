using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Infrastructure.Enums.Verifications;

namespace Infrastructure.Entities.Others
{

    [PrimaryKey(nameof(Id))]
    public class VerificationRequest : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Code { get; set; } = null!;
        public bool Confirmed { get; set; } = false;
        public VerificationType Type { get; set; }
        public DateTime ExpiresAt { get; set; }


        public User? User { get; set; }
    }
}
