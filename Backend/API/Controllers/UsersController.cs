using API.Utils;
using Core.IServices.Users;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;
using Microsoft.AspNetCore.Mvc;

namespace Fly.SMS.API.Controllers
{

    [ProducesResponseType(200, Type = typeof(MessageResponse))]
    [ProducesResponseType(404, Type = typeof(MessageResponse))]
    [ProducesResponseType(400, Type = typeof(MessageResponse))]
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        private readonly IUserService _userService;

        public UsersController(ITranslationService translationService, IUserService userService)
        {
            _translationService = translationService;
            _userService = userService;
        }


        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<UserDTOs.Responses.GetAllDT>>))]
        [HttpPost, Route("GetAllDT")]
        public async Task<IActionResult> GetAllDT(DataTableDTOs.UsersDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            (int count, IEnumerable<UserDTOs.Responses.GetAllDT> list) data = await _userService.GetAllDT(dto);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, "User");
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }


        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<UserDTOs.Responses.GetById>))]
        [HttpGet, Route("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            UserDTOs.Responses.GetById data = await _userService.GetById(id);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, "User");
            return Ok(MessageResponseFactory.Create(message, data));
        }




        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(UserDTOs.Requsts.Create dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Create(dto);

            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }

        [HttpPost, Route("Edit")]
        public async Task<IActionResult> Edit(UserDTOs.Requsts.Edit dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Edit(dto);

            string message = _translationService.GetByKey(TranslationKeys.Edited, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }

        [HttpDelete, Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Delete(id);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }



        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.Login>))]
        [ProducesResponseType(202, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.Login2FA>))]
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(UserDTOs.Requsts.Login dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            UserDTOs.Responses.BaseLogin response = await _userService.Login(dto);
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
        public async Task<IActionResult> VerifyCode(UserDTOs.Requsts.VerifiyCode dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            UserDTOs.Responses.VerifyCode? response = await _userService.VerifyCode(dto.requestId, dto.code);
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
        public async Task<IActionResult> SendVerificationAgain(UserDTOs.Requsts.SendVerificationCodeAgain dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            Guid response = await _userService.SendVerificationAgain(dto);
            string message = _translationService.GetByKey("SendVerificationAgain", langKey);
            return Ok(MessageResponseFactory.Create(message, new UserDTOs.Responses.SendVerificationAgain { requestId = response }));
        }






        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.SendVerification>))]
        [HttpPost, Route("SendVerification")]
        public async Task<IActionResult> SendVerification(UserDTOs.Requsts.SendVerification dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            UserDTOs.Responses.SendVerification response = await _userService.SendVerificationRequest(dto);
            string message = _translationService.GetByKey(TranslationKeys.Success, langKey, "SendVerification");
            return Ok(MessageResponseFactory.Create(message, response));
        }





        [ProducesResponseType(200, Type = typeof(MessageResponse))]
        [HttpPost, Route("SetPassword")]
        public async Task<IActionResult> SetPassword(UserDTOs.Requsts.SetPassword dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.SetPassword(dto);
            string message = _translationService.GetByKey(TranslationKeys.Success, langKey, "SetPassword");
            return Ok(MessageResponseFactory.Create(message));
        }




    }
}
