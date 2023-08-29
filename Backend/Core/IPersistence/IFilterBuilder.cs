using System.Linq.Expressions;

namespace Core.IPersistence
{
    public interface IFilterBuilder<T>
    {
        IFilterBuilder<T> Add(Expression<Func<T, bool>> filter);
        IEnumerable<Expression<Func<T, bool>>> Build();
    }
}
