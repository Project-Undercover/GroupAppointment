using Infrastructure.Entities.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IPersistence.IRepositories.Others
{
    public interface IVerificationRequestRepository : IRepository<VerificationRequest>
    {
        Task<int> CountAttemptsAsync(Guid userId, int minutes);
    }
}
