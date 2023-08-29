namespace Infrastructure.Entities.DataTables
{
    public class DataTableDTOs
    {
        public record Paginate(int skip = 0, int take = -1, List<Orderable>? Orderables = null);
        public record Orderable(string? fieldName = null, int? dir = null);

        public record UsersDT(Searches.UserDT? CustomSearch) : Paginate;
        public record SessionDT(Searches.SessionDT? CustomSearch) : Paginate;

    }
}
