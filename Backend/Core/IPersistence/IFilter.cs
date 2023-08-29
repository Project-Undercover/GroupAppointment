using Infrastructure.Entities.Shared;
using System.Linq.Expressions;

namespace Core.IPersistence
{
    public interface IFilter<T>
    {
        List<string> Includes { get; }
        List<Expression<Func<T, bool>>> Filters { get; }

        IFilter<T> Add(Expression<Func<T, bool>> filter);
        IFilter<T> Include(string field);
    }
}
