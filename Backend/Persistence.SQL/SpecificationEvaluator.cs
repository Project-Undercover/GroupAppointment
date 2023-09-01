using Core.IPersistence;
using Infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SQL
{
    public class SpecificationEvaluator<TEntity> where TEntity : Entity
    {
        public static async Task<(int, IQueryable<TEntity>)> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criterias != null)
            {
                foreach (var criteria in specification.Criterias)
                {
                    query = query.Where(criteria);
                }
            }

            int count = await query.CountAsync();

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.Orderings != null)
            {
                query = specification.Orderings(query);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take == -1 ? int.MaxValue : specification.Take);
            }
            return (count, query);
        }
    }
}
