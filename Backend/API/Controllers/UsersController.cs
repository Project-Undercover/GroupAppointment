using API.Utils;
using Core.IServices.Users;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using static API.Middlewares.Authorization;
using static Infrastructure.Enums.Enums;

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


        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<UserDTOs.Responses.GetAllDT>>))]
        [HttpPost, Route("GetAllDT")]
        public async Task<IActionResult> GetAllDT(DataTableDTOs.UsersDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            (int count, IEnumerable<UserDTOs.Responses.GetAllDT> list) data = await _userService.GetAllDT(dto);

            data.list.ToList().ForEach(s => s.roleName = _translationService.GetByKey(s.roleName, langKey));

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, "User");
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }


        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.GetById>))]
        [HttpGet, Route("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            UserDTOs.Responses.GetById data = await _userService.GetById(id);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, "User");
            return Ok(MessageResponseFactory.Create(message, data));
        }



        [AuthorizeUser]
        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(UserDTOs.Requests.Create dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Create(dto);

            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }


        [AuthorizeUser]
        [HttpPost, Route("Edit")]
        public async Task<IActionResult> Edit(UserDTOs.Requests.Edit dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Edit(dto);

            string message = _translationService.GetByKey(TranslationKeys.Edited, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }


        [AuthorizeUser]
        [HttpDelete, Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Delete(id);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }



        [AuthorizeUser]
        [HttpPost, Route("AddChild")]
        public async Task<IActionResult> AddChild(UserDTOs.Requests.AddChild dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            await _userService.AddChild(dto);
            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, nameof(Child));
            return Ok(MessageResponseFactory.Create(message));
        }

        [AuthorizeUser]
        [HttpDelete, Route("DeleteChild/{id}")]
        public async Task<IActionResult> DeleteChild(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            await _userService.DeleteChild(id);
            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, nameof(Child));
            return Ok(MessageResponseFactory.Create(message));
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
