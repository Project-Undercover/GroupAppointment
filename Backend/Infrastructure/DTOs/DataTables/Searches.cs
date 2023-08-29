namespace Infrastructure.Entities.DataTables
{
    public class Searches
    {
        public record UserDT(string? name, string? email, bool? isActive, string? mobileNumber);

    }
}
