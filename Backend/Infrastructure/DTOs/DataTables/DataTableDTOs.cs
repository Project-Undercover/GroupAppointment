using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Shared;
using Infrastructure.Exceptions;
using Infrastructure.Utils;
using System.Xml.Linq;
using static Infrastructure.DTOs.DataTables.DataTableDTOs;

namespace Infrastructure.DTOs.DataTables
{



    public class DataTableDTOs
    {

        public interface IOrderable<T> where T : Entity
        {
            IOrderedQueryable<T> DataOrdering(IQueryable<T> func);
        }

        public record Orderable(string fieldName, int dir = -1);
        public record Paginate : IOrderable<Entity>
        {
            public int skip { get; set; } = 0;
            public int take { get; set; } = -1;
            public List<Orderable>? Orderables = null;

            public virtual IOrderedQueryable<Entity> DataOrdering(IQueryable<Entity> func)
            {
                return func.OrderByDescending(s => s.CreatedAt);
            }
        }

        public record UsersDT(Searches.UserDT? CustomSearch) : Paginate;
        public record SessionDT(Searches.SessionDT? CustomSearch) : Paginate;
        public record UserSessionDT(Searches.UserSessionDT? CustomSearch) : Paginate;


        public record SessionDTT : Paginate
        {
            public Searches.SessionDT? CustomSearch { get; set; }

            //public override IOrderedQueryable<Session> DataOrdering(IQueryable<Session> func)
            //{
            //    if (Orderables is null) // use default ordering
            //        return base.DataOrdering(func);

            //    var query = func;


            //    // incoming ordering
            //    for (int i = 0; i < Orderables.Count(); i++)
            //    {
            //        query = DataOrderingHelper(query, Orderables[i]);
            //    }

            //    return (IOrderedQueryable<Session>)query;
            //}




            //IOrderedQueryable<Session> DataOrderingHelper(IQueryable<Session> func, Orderable orderable)
            //{
            //    switch (orderable.fieldName)
            //    {
            //        case "Hello":
            //            return func.OrderByOrThenBy(s => s.CreatedAt, orderable);

            //        case "Hello23":
            //            return func.OrderByOrThenBy(s => s.CreatedAt, orderable);

            //        default: throw new ValidationException("WrongOrderingField");
            //    }
            //}
        }

    }
}
