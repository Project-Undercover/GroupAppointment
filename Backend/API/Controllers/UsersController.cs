using API.Files;
using API.Utils;
using Core.IServices.Users;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Users;
using Infrastructure.Exceptions;
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
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.HomeData>))]
        [HttpPost, Route("HomeData")]
        public async Task<IActionResult> HomeData(UserDTOs.Requests.HomeData dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            User user = HttpContext.GetUser<User>();

            UserDTOs.Responses.HomeData data = await _userService.GetHomeData(dto, user);

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }



        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<UserDTOs.Responses.GetAllDT>>))]
        [HttpPost, Route("GetAllDT")]
        public async Task<IActionResult> GetAllDT(DataTableDTOs.UsersDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            User user = HttpContext.GetUser<User>();

            (int count, IEnumerable<UserDTOs.Responses.GetAllDT> list) data = await _userService.GetAllDT(dto, user);

            data.list.ToList().ForEach(s => s.roleName = _translationService.GetByKey(s.roleName, langKey));

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }



        /// <summary>
        /// Get User Children
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<List<UserDTOs.Responses.Child>>))]
        [HttpPost, Route("GetChildren")]
        public async Task<IActionResult> GetChildren()
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            User user = HttpContext.GetUser<User>();

            IEnumerable<UserDTOs.Responses.Child> data = await _userService.GetChildren(user);

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }




        /// <summary>
        /// admin only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UnAuthorizedException"></exception>
        [AuthorizeUser(UserRole.Admin)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.GetById>))]
        [HttpGet, Route("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            UserDTOs.Responses.GetById data = await _userService.GetById(id);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }


        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<UserDTOs.Responses.GetById>))]
        [HttpGet, Route("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            User user = HttpContext.GetUser<User>();
            string langKey = Headers.GetLanguage(Request.Headers);

            UserDTOs.Responses.GetById data = await _userService.GetById(user.Id);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }



        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
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
            User user = HttpContext.GetUser<User>();
            if (!user.IsAdmin && dto.Id != user.Id) throw new UnAuthorizedException(TranslationKeys.UnAuthorized);


            string langKey = Headers.GetLanguage(Request.Headers);

            await _userService.Edit(dto);

            string message = _translationService.GetByKey(TranslationKeys.Edited, langKey, "User");
            return Ok(MessageResponseFactory.Create(message));
        }


        /// <summary>
        /// admin only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
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
            User user = HttpContext.GetUser<User>();
            if (!user.IsAdmin && dto.userId != user.Id) throw new UnAuthorizedException(TranslationKeys.UnAuthorized);


            string langKey = Headers.GetLanguage(Request.Headers);
            await _userService.AddChild(dto);
            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, nameof(Child));
            return Ok(MessageResponseFactory.Create(message));
        }

        [AuthorizeUser]
        [HttpDelete, Route("DeleteChild/{id}")]
        public async Task<IActionResult> DeleteChild(Guid id)
        {
            User user = HttpContext.GetUser<User>();
            if (!user.IsAdmin && !user.Children.Any(s => s.Id == id)) throw new UnAuthorizedException(TranslationKeys.UnAuthorized);

            string langKey = Headers.GetLanguage(Request.Headers);
            await _userService.DeleteChild(id);
            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, nameof(Child));
            return Ok(MessageResponseFactory.Create(message));
        }


        [AuthorizeUser]
        [HttpPost, Route("EditProfileImage")]
        public async Task<IActionResult> EditProfileImage(IFormFile imageFile)
        {
            User user = HttpContext.GetUser<User>();

            string langKey = Headers.GetLanguage(Request.Headers);
            await _userService.EditProfileImage(user.Id, new AzureFormFileProxy(imageFile));
            string message = _translationService.GetByKey(TranslationKeys.Edited, langKey, "ProfileImage");
            return Ok(MessageResponseFactory.Create(message));
        }

    }
}
