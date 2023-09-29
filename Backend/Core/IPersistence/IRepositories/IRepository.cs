using System.Linq.Expressions;

namespace Core.IPersistence.IRepositories
{
    public interface IRepository<TEntity>
    {
        IFilter<TEntity> GetFilter { get; }
        ISpecification<TEntity> QuerySpecification { get; }


        Task<(int, IEnumerable<TEntity>)> Find(ISpecification<TEntity>? specification = null);
        Task<(int count, IEnumerable<TEntity> data)> Find(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1, params string[] includes);
        Task<(int count, IEnumerable<TEntity> data)> Find(IFilter<TEntity>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int skip = 0, int take = -1);
        Task<TEntity> GetByIdAsync(Guid id, params string[] includes);
        Task<TEntity> GetByAsync(IFilter<TEntity> filter);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter, params string[] includes);



        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entityToDelete);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entityToUpdate, params string[] exclude);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
    }
}
