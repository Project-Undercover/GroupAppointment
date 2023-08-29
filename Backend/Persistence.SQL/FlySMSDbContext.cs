using Infrastructure.Entities.Others;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SQL
{
    public class FlySMSDbContext : DbContext
    {
        public FlySMSDbContext(DbContextOptions<FlySMSDbContext> options) : base(options)
        {
        }


        public virtual DbSet<VerificationRequest> VerificationRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
