using System.Linq.Expressions;

namespace Core.IPersistence.IRepositories
{
    public interface IRepository<TEntity>
    {
        IFilterBuilder<TEntity> GetFilterBuilder { get; }


        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="filters">for filtering out the returned data</param>
        /// <param name="orderBy">to order the returned data</param>
        /// <param name="page">current page number, starts from 1</param>
        /// <param name="pageSize">the size of each page</param>
        /// <param name="populate">populate a specific value, Known as (Join in relation databases)</param>
        /// <returns></returns>
        Task<(int count, IEnumerable<TEntity> data)> GetAsync(
            IEnumerable<Expression<Func<TEntity, bool>>>? filters = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = -1,
            params string[] populate);



        /// <summary>
        /// Get the entity by its defined Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync<T>(T id);


        Task<TEntity> GetByIdAsync(Guid id);


        /// <summary>
        /// Gets the entity by a custom lambda exprision
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter);


        /// <summary>
        /// Add the entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Delete the enetity from the database
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entityToDelete);

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entityToUpdate, params string[] exclude);

        /// <summary>
        /// Check if exists by a specific field
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);


        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
    }
}
