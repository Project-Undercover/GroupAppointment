using Core.IPersistence;
using System.Linq.Expressions;

namespace Persistence.SQL
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {

        }
        public List<Expression<Func<T, bool>>> Criterias { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
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

        public ISpecification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
            return this;
        }

        public ISpecification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
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
    }
}
