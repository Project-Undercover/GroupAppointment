﻿using API.Utils;
using Core.IServices.Auth;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    [ProducesResponseType(200, Type = typeof(MessageResponse))]
    [ProducesResponseType(404, Type = typeof(MessageResponse))]
    [ProducesResponseType(400, Type = typeof(MessageResponse))]
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ITranslationService _translationService;
        private readonly IAuthService _authService;

        public AuthController(ITranslationService translationService, IAuthService authService)
        {
            _translationService = translationService;
            _authService = authService;
        }






        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.Login>))]
        [ProducesResponseType(202, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.Login2FA>))]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(UserDTOs.Requests.Login dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            UserDTOs.Responses.BaseLogin response = await _authService.Login(dto);
            string message = _translationService.GetByKey(TranslationKeys.Success, langKey, "Login");
            if (response is UserDTOs.Responses.Login) // no 2FA
                return Ok(MessageResponseFactory.Create(message, (UserDTOs.Responses.Login)response));

            return StatusCode(StatusCodes.Status202Accepted, MessageResponseFactory.Create(message, (UserDTOs.Responses.Login2FA)response));
        }

        /// <summary>
        /// Verify Code
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.VerifyCode>))]
        [HttpPost, Route("VerifyCode")]
        public async Task<IActionResult> VerifyCode(UserDTOs.Requests.VerifiyCode dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            UserDTOs.Responses.VerifyCode? response = await _authService.VerifyCode(dto.requestId, dto.code);
            string message = _translationService.GetByKey(TranslationKeys.Success, langKey, "VerifyRequest");
            return Ok(MessageResponseFactory.Create(message, response));
        }

        /// <summary>
        /// Send Verification Code Again
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.SendVerificationAgain>))]
        [HttpPost, Route("SendVerificationAgain")]
        public async Task<IActionResult> SendVerificationAgain(UserDTOs.Requests.SendVerificationCodeAgain dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            Guid response = await _authService.SendVerificationAgain(dto);
            string message = _translationService.GetByKey("SendVerificationAgain", langKey);
            return Ok(MessageResponseFactory.Create(message, new UserDTOs.Responses.SendVerificationAgain { requestId = response }));
        }





        //[ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.SendVerification>))]
        //[HttpPost, Route("SendVerification")]
        //public async Task<IActionResult> SendVerification(UserDTOs.Requests.SendVerification dto)
        //{
        //    string langKey = Headers.GetLanguage(Request.Headers);

        //    UserDTOs.Responses.SendVerification response = await _userService.SendVerificationRequest(dto);
        //    string message = _translationService.GetByKey(TranslationKeys.Success, langKey, "SendVerification");
        //    return Ok(MessageResponseFactory.Create(message, response));
        //}
    }
}
