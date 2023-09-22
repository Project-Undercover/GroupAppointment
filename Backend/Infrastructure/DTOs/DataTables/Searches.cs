using static Infrastructure.Enums.Enums;

namespace Infrastructure.DTOs.DataTables
{
    public class Searches
    {
        public record UserDT(string? name, string? email, bool? isActive, string? mobileNumber, List<UserRole>? roles, bool? withCurrentUser);
        public record SessionDT(string? searchTerm, DateTime? startDate, DateTime? endDate);
        public record UserSessionDT(DateTime? startDate, DateTime? endDate, string? searchTerm = null);
    }
}
