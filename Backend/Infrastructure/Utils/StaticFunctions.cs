using Infrastructure.Entities.Sessions;
using Infrastructure.Utils;
using System.Linq.Expressions;
using System.Xml.Linq;
using static Infrastructure.Entities.DataTables.DataTableDTOs;

namespace Infrastructure.Utils
{
    public static class StaticFunctions
    {
        public static string GenerateRandomCode(int? length = Constants.VerificationCodeLength)
        {
            bool? isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Contains("Development");
            //if (isDevelopment is null || (isDevelopment.HasValue && isDevelopment.Value))
            return "11111";

            var random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length.Value).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static IOrderedQueryable<TElement> OrderByOrThenBy<TElement, TKey>(
             this IQueryable<TElement> query,
             Expression<Func<TElement, TKey>> ordering,
             Orderable orderable)
        {
            var ordered = query as IOrderedQueryable<TElement>;

            if (ordered == null)
                return orderable.dir == 1 ? query.OrderBy(ordering) : query.OrderByDescending(ordering);

            return orderable.dir == 1 ? ordered.ThenBy(ordering) : ordered.ThenByDescending(ordering);
        }
    }
}
