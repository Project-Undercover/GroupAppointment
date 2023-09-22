using API.Utils;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Enums;
using Microsoft.AspNetCore.Mvc;
using static API.Middlewares.Authorization;
using static Infrastructure.Enums.Enums;

namespace API.Controllers
{
    [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<List<EnumDTOs.EnumValues>>))]
    [ProducesResponseType(404, Type = typeof(MessageResponse))]
    [ProducesResponseType(400, Type = typeof(MessageResponse))]
    [AuthorizeUser(UserRole.Admin)]
    [Route("/api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {


        private readonly ITranslationService _translationService;
        private readonly IEnumService _enumService;

        public EnumsController(ITranslationService translationService, IEnumService enumService)
        {
            _translationService = translationService;
            _enumService = enumService;
        }



        [HttpGet, Route("Roles")]
        public async Task<IActionResult> Roles()
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            List<EnumDTOs.EnumValues> data = _enumService.GetValues(typeof(UserRole), langKey);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }



        [HttpGet, Route("SessionStatuses")]
        public async Task<IActionResult> SessionStatuses()
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            List<EnumDTOs.EnumValues> data = _enumService.GetValues(typeof(SessionStatus), langKey);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }


    }
}
