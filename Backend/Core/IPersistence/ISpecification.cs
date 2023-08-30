using System.Linq.Expressions;

namespace Core.IPersistence
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criterias { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        //List<Expression<Func<T, object>>> Selections { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }



        //public ISpecification<T> Select(Expression<Func<T, bool>> selection);
        public ISpecification<T> Where(Expression<Func<T, bool>> codition);
        public ISpecification<T> Include(Expression<Func<T, object>> includeExpression);
        public ISpecification<T> Include(string includeString);
        public ISpecification<T> SkipAndTake(int skip, int take);
        public ISpecification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression);
        public ISpecification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression);
        public ISpecification<T> ApplyGroupBy(Expression<Func<T, object>> groupByExpression);
    }
}
