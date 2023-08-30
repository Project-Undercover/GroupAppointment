using Core.IPersistence;
using Core.IPersistence.IRepositories;
using Core.IPersistence.IRepositories.Others;
using Infrastructure.Entities.Shared;
using Persistence.SQL.Repositories;
using Persistence.SQL.Repositories.Others;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Persistence.SQL
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;
        private Hashtable? _repositories;


        public UnitOfWork(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IVerificationRequestRepository VerificationRequests => new VerificationRequestRepository(_context);


        public async Task<int> Commit(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in _context.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UpdateDate(DateTime.Now);
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDate(DateTime.Now);
                        break;
                }
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<T> Repository<T>() where T : Entity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            string type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type]!;
        }

    }
}
