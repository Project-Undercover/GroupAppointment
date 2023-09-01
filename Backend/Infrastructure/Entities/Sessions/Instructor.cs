using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Sessions
{
    public class Instructor : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Session))]
        public Guid SessionId { get; set; }


        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User User { get; set; }

        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Session Session { get; set; }
    }
}
