using API.Files;
using API.Utils;
using Core.IServices.Sessions;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.DTOs.DataTables;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.Users;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static API.Middlewares.Authorization;
using static Infrastructure.DTOs.Sessions.SessionsDTOs.Requests;
using static Infrastructure.DTOs.Sessions.SessionsDTOs.Responses;
using static Infrastructure.Enums.Enums;

namespace API.Controllers
{
    [ProducesResponseType(200, Type = typeof(MessageResponse))]
    [ProducesResponseType(404, Type = typeof(MessageResponse))]
    [ProducesResponseType(400, Type = typeof(MessageResponse))]
    [Route("/api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {

        private readonly ITranslationService _translationService;
        private readonly ISessionService _sessionService;

        public SessionsController(ITranslationService translationService, ISessionService sessionService)
        {
            _translationService = translationService;
            _sessionService = sessionService;
        }




        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<GetAllDT>>))]
        [HttpPost, Route("GetAllDT")]
        public async Task<IActionResult> GetAllDT(DataTableDTOs.SessionDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            User user = HttpContext.GetUser<User>();

            (int count, IEnumerable<GetAllDT> list) data = await _sessionService.GetAllDT(dto, user);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }


        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<GetAllDT>>))]
        [HttpPost, Route("GetUserSessions")]
        public async Task<IActionResult> GetUserSessions(DataTableDTOs.UserSessionDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            User user = HttpContext.GetUser<User>();
            (int count, IEnumerable<UserSession> list) data = await _sessionService.GetUserSessions(dto, user);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }


        [AuthorizeUser(UserRole.Admin, UserRole.Instructor)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<IEnumerable<SessionsDTOs.Responses.Child>>))]
        [HttpGet, Route("GetSessionParticipants")]
        public async Task<IActionResult> GetSessionParticipants(Guid sessionId)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            IEnumerable<SessionsDTOs.Responses.Child> data = await _sessionService.GetSessionParticipants(sessionId);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }


        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<GetById>))]
        [HttpGet, Route("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            GetById data = await _sessionService.GetById(id);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message, data));
        }



        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<List<SessionsDTOs.Responses.Instructor>>))]
        [HttpGet, Route("GetInstructors")]
        public async Task<IActionResult> GetInstructors()
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            List<SessionsDTOs.Responses.Instructor> data = await _sessionService.GetInstructors();
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }




        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
        [HttpPost, Route("Create")]
        public async Task<IActionResult> Create(IFormFile? imageFile, [FromForm] Create dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            IFileProxy? file = imageFile != null ? new AzureFormFileProxy(imageFile) : null;
            await _sessionService.Create(file, dto);

            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message));
        }

        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
        [HttpPost, Route("Edit")]
        public async Task<IActionResult> Edit(IFormFile? imageFile, [FromForm] Edit dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            IFileProxy? file = imageFile != null ? new AzureFormFileProxy(imageFile) : null;
            await _sessionService.Edit(file, dto);

            string message = _translationService.GetByKey(TranslationKeys.Edited, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message));
        }


        /// <summary>
        /// Admin only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeUser(UserRole.Admin)]
        [HttpDelete, Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _sessionService.Delete(id);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message));
        }



        [AuthorizeUser(allowAll = true)]
        [HttpPost, Route("AddParticipants")]
        public async Task<IActionResult> AddParticipants(AddParticipant dto)
        {
            User user = HttpContext.GetUser<User>();
            if (!user.IsAdmin && dto.Children.Any(s => !user.Children.Select(s => s.Id).Contains(s))) throw new ForbiddenException(TranslationKeys.UnAuthorized);

            string langKey = Headers.GetLanguage(Request.Headers);

            await _sessionService.AddParticipants(dto);

            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, nameof(Participant));
            return Ok(MessageResponseFactory.Create(message));
        }



        [AuthorizeUser(allowAll = true)]
        [HttpDelete, Route("DeleteParticipant/{id}")]
        public async Task<IActionResult> DeleteParticipant(Guid id)
        {
            User user = HttpContext.GetUser<User>();

            string langKey = Headers.GetLanguage(Request.Headers);

            await _sessionService.DeleteParticipant(id, user);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, nameof(Participant));
            return Ok(MessageResponseFactory.Create(message));
        }


        [AuthorizeUser(allowAll = true)]
        [HttpDelete, Route("DeleteUserSessionParticipants/{sessionId}")]
        public async Task<IActionResult> DeleteUserSessionParticipants(Guid sessionId)
        {
            User user = HttpContext.GetUser<User>();
            string langKey = Headers.GetLanguage(Request.Headers);

            await _sessionService.DeleteUserSessionParticipants(sessionId, user);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, "Participants");
            return Ok(MessageResponseFactory.Create(message));
        }
    }
}
