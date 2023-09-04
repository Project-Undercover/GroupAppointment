using Infrastructure.Entities.Others;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public virtual DbSet<VerificationRequest> VerificationRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }


    }
}
