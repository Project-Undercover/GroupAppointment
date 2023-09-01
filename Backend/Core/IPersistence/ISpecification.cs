using System.Linq.Expressions;

namespace Core.IPersistence
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criterias { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        //List<Expression<Func<T, object>>> Selections { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> GroupBy { get; }
        Func<IQueryable<T>, IOrderedQueryable<T>>? Orderings { get; }


        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }


        //public ISpecification<T> Select(Expression<Func<T, bool>> selection);
        public ISpecification<T> Where(Expression<Func<T, bool>> codition);
        public ISpecification<T> Include(Expression<Func<T, object>> includeExpression);
        public ISpecification<T> Include(string includeString);
        public ISpecification<T> SkipAndTake(int skip, int take);

        public ISpecification<T> ApplyOrderings(Func<IQueryable<T>, IOrderedQueryable<T>> orderings);
        public ISpecification<T> ApplyGroupBy(Expression<Func<T, object>> groupByExpression);
    }
}
