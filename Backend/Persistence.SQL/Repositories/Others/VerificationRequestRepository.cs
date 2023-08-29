using Core.IPersistence.IRepositories.Others;
using Infrastructure.Entities.Others;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SQL.Repositories.Others
{
    public class VerificationRequestRepository : Repository<VerificationRequest>, IVerificationRequestRepository
    {
        private readonly FlySMSDbContext _context;
        private readonly DbSet<VerificationRequest> _set;


        public VerificationRequestRepository(FlySMSDbContext context) : base(context)
        {
            _context = context;
            _set = context.Set<VerificationRequest>();
        }

        public async Task<int> CountAttemptsAsync(Guid userId, int minutes)
        {
            return await _set.CountAsync(s => !s.Confirmed && s.UserId == userId && EF.Functions.DateDiffMinute(s.CreatedAt, DateTime.Now) < minutes);
        }
    }
}
