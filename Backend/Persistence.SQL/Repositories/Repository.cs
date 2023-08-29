using Core.IPersistence.IRepositories;
using Core.IPersistence;
using Infrastructure.Entities.Shared;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.SQL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public class FilterBuilder : IFilterBuilder<TEntity>
        {
            private readonly List<Expression<Func<TEntity, bool>>> filters;
            public FilterBuilder()
            {
                filters = new List<Expression<Func<TEntity, bool>>>();
            }

            public IFilterBuilder<TEntity> Add(Expression<Func<TEntity, bool>> filter)
            {
                filters.Add(filter);
                return this;
            }


            public IEnumerable<Expression<Func<TEntity, bool>>> Build()
            {
                return filters;
            }
        }



        public IFilterBuilder<TEntity> GetFilterBuilder => new FilterBuilder();

        public async Task AddAsync(TEntity entity)
        {
            _dbContext.Database.EnsureCreated();
            await _dbSet.AddAsync(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbContext.Database.EnsureCreated();
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<(int count, IEnumerable<TEntity> data)> GetAsync(IEnumerable<Expression<Func<TEntity, bool>>>? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] populate)
        {
            _dbContext.Database.EnsureCreated();
            IQueryable<TEntity> query = _dbSet;

            if (filters != null)
                foreach (var f in filters)
                    query = query.Where(f);

            foreach (var includeProperty in populate)
            {
                query = query.Include(includeProperty);
            }

            int count = await query.CountAsync();

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);
            return (count, await query.ToListAsync());
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            _dbContext.Database.EnsureCreated();
            TEntity? data = await _dbSet.FirstOrDefaultAsync(s => s.Id == id);
            return data ?? throw new NotFoundException("NotFound", typeof(TEntity).Name);
        }

        public async Task<TEntity> GetByIdAsync<T>(T id)
        {
            _dbContext.Database.EnsureCreated();
            TEntity? data = await _dbSet.FindAsync(id);
            return data ?? throw new NotFoundException("NotFound", typeof(TEntity).Name);
        }

        public async Task UpdateAsync(TEntity entity, params string[] exclude)
        {
            _dbContext.Database.EnsureCreated();

            _dbContext.Entry(entity).Property(nameof(Entity.Id)).IsModified = false;
            _dbContext.Entry(entity).Property(nameof(Entity.CreatedAt)).IsModified = false;

            foreach (var property in exclude)
            {
                _dbContext.Entry(entity).Property(property).IsModified = false;
            }
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            return await query.AnyAsync(filter);
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            TEntity? data = await query.FirstOrDefaultAsync(filter);
            return data ?? throw new NotFoundException("NotFound", typeof(TEntity).Name);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }
    }
}
