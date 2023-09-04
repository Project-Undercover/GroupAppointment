using Core.IUtils;
using Infrastructure.DTOs.Enums;

namespace Services.Utils
{
    public class EnumsService: IEnumService
    {
        private readonly ITranslationService _translationService;

        public EnumsService(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        public List<EnumDTOs.EnumValues> GetValues(Type type, string langKey)
        {
            var response = new List<EnumDTOs.EnumValues>();
            var enumValues = type.GetEnumValues();
            foreach (var item in enumValues)
            {
                response.Add(new EnumDTOs.EnumValues
                {
                    name = _translationService.GetByKey(item.ToString(), langKey),
                    value = (int)item
                });
            }
            return response;
        }
    }
}
