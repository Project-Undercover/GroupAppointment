using Infrastructure.DTOs.Enums;

namespace Core.IUtils
{
    public interface IEnumService
    {
        List<EnumDTOs.EnumValues> GetValues(Type type, string langKey);
    }
}
