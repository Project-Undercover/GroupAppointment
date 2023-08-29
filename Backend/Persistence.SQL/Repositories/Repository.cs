using Core.IPersistence.IRepositories;
using Core.IPersistence;
using Infrastructure.Entities.Shared;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Infrastructure.Entities.Users;

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










        public class FilterBuilder : IFilter<TEntity>
        {
            private readonly List<string> _includes;
            private readonly List<Expression<Func<TEntity, bool>>> _filters;

            public List<string> Includes => _includes;
            public List<Expression<Func<TEntity, bool>>> Filters => _filters;

            public FilterBuilder()
            {
                _filters = new List<Expression<Func<TEntity, bool>>>();
                _includes = new List<string>();
            }

            public IFilter<TEntity> Add(Expression<Func<TEntity, bool>> filter)
            {
                _filters.Add(filter);
                return this;
            }

            public IFilter<TEntity> Include(string include)
            {
                _includes.Add(include);
                return this;
            }
        }



        public IFilter<TEntity> GetFilter => new FilterBuilder();

        public async Task<(int count, IEnumerable<TEntity> data)> Find(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] inlcluds)
        {
            _dbContext.Database.EnsureCreated();
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
                query.Where(filter);

            foreach (var includeProperty in inlcluds)
                query = query.Include(includeProperty);

            int count = await query.CountAsync();

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);
            return (count, await query.ToListAsync());
        }
        public async Task<(int count, IEnumerable<TEntity> data)> Find(IFilter<TEntity>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1)
        {
            _dbContext.Database.EnsureCreated();
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                foreach (var includeProperty in filter.Includes)
                    query = query.Include(includeProperty);

                foreach (var f in filter.Filters)
                    query = query.Where(f);
            }

            int count = await query.CountAsync();

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip).Take(take == -1 ? int.MaxValue : take);
            return (count, await query.ToListAsync());
        }
        public async Task<TEntity> GetByAsync(IFilter<TEntity> filter)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (filter != null)
            {
                foreach (var includeProperty in filter.Includes)
                    query = query.Include(includeProperty);

                foreach (var f in filter.Filters)
                    query = query.Where(f);
            }


            return await query.FirstOrDefaultAsync() ?? throw new NotFoundException("NotFound", typeof(TEntity).Name);
        }
        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter, params string[] inlcluds)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            foreach (var includeProperty in inlcluds)
                query = query.Include(includeProperty);

            TEntity? data = await query.FirstOrDefaultAsync(filter);
            return data ?? throw new NotFoundException("NotFound", typeof(TEntity).Name);
        }
        public async Task<TEntity> GetByIdAsync(Guid id, params string[] inlcluds)
        {
            _dbContext.Database.EnsureCreated();
            IQueryable<TEntity>? query = _dbSet.AsQueryable();

            foreach (var includeProperty in inlcluds)
                query = query.Include(includeProperty);

            return await query.FirstOrDefaultAsync(s => s.Id == id) ?? throw new NotFoundException("NotFound", typeof(TEntity).Name);
        }



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



        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }
    }
}
