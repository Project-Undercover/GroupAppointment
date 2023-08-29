namespace Infrastructure.Entities.DataTables
{
    public class Searches
    {
        public record UserDT(string? name, string? email, bool? isActive, string? mobileNumber);
        public record SessionDT(Guid? userId, string? participantName, DateTime? startDate, DateTime? endDate);

    }
}
