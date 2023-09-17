using API.Utils;
using Core.IServices.Others;
using Core.IServices.Users;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Others;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using static API.Middlewares.Authorization;
using static Infrastructure.Enums.Enums;

namespace API.Controllers
{
    [ProducesResponseType(200, Type = typeof(MessageResponse))]
    [ProducesResponseType(404, Type = typeof(MessageResponse))]
    [ProducesResponseType(400, Type = typeof(MessageResponse))]
    [AuthorizeUser(UserRole.Admin)]
    [Route("/api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly ITranslationService _translationService;
        private readonly IFCMService _fcmService;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(ITranslationService translationService, IFCMService fcmService, ILogger<NotificationController> logger)
        {
            _translationService = translationService;
            _fcmService = fcmService;
            _logger = logger;
        }


        [ProducesResponseType(200, Type = typeof(MessageResponse))]
        [HttpPost, Route("SendNotification")]
        public async Task<IActionResult> SendNotification(FCMDTOs.Request.SendNotification dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            var fcmHE_DTO = new FCMDTOs.Response.NotificationBody { message = dto.text_HE, Title = dto.text_HE, subtitle = "" };
            var fcmAR_DTO = new FCMDTOs.Response.NotificationBody { message = dto.text_AR, Title = dto.text_AR, subtitle = "" };

            await _fcmService.SendAllNotificationAsync(fcmAR_DTO, "AR");
            await _fcmService.SendAllNotificationAsync(fcmHE_DTO, "HE");
            return Ok(_translationService.GetByKey(TranslationKeys.Success, langKey, "NotificationSent"));
        }
    }
}
