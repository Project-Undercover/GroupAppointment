using Core.IPersistence.IRepositories;
using Core.IPersistence.IRepositories.Others;
using Infrastructure.Entities.Shared;

namespace Core.IPersistence
{
    public interface IUnitOfWork
    {
        IVerificationRequestRepository VerificationRequests { get; }

        Task<int> Commit(CancellationToken cancellationToken = default);
        IRepository<T> Repository<T>() where T : Entity;
    }
}
