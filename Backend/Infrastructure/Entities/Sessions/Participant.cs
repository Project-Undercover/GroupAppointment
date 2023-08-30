using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Sessions
{
    public class Participant : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [ForeignKey(nameof(Session))]
        public Guid SessionId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }


        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Session Session { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User User { get; set; }
    }
}
