using Core.IPersistence;
using Infrastructure.Entities.Shared;
using System.Linq.Expressions;

namespace Persistence.SQL
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {

        }
        public List<Expression<Func<T, bool>>> Criterias { get; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Func<IQueryable<T>, IOrderedQueryable<T>>? Orderings { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        public ISpecification<T> Include(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }

        public ISpecification<T> Include(string includeString)
        {
            if (IncludeStrings.Contains(includeString)) return this;

            IncludeStrings.Add(includeString);
            return this;
        }

        public ISpecification<T> SkipAndTake(int skip, int take)
        {
            if (IsPagingEnabled)
                throw new Exception("Paging Already applied");
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
            return this;
        }

        public ISpecification<T> ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
            return this;
        }

        public ISpecification<T> Where(Expression<Func<T, bool>> codition)
        {
            Criterias.Add(codition);
            return this;
        }

        public ISpecification<T> ApplyOrderings(Func<IQueryable<T>, IOrderedQueryable<T>> orderings)
        {
            Orderings = orderings;
            return this;
        }
    }
}
