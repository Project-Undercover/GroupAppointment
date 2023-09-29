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
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using static API.Middlewares.Authorization;
using static Infrastructure.DTOs.Sessions.SessionsDTOs.Requests;
using static Infrastructure.DTOs.Sessions.SessionsDTOs.Responses;
using static Infrastructure.Enums.Enums;

namespace API.Controllers
{
    [ProducesResponseType(200, Type = typeof(MessageResponse))]
    [ProducesResponseType(404, Type = typeof(MessageResponse))]
    [ProducesResponseType(400, Type = typeof(MessageResponse))]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
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


        //private string TranslateStatus(User user, bool isParticipating, SessionStatusDTO status, string langKey)
        //{
        //    if (user.IsAdmin)
        //        return _translationService.GetByKey(status.name, langKey);

        //    if (isParticipating && (status.value == SessionStatus.Available || status.value == SessionStatus.Full))
        //        return _translationService.GetByKey("Participating", langKey);

        //    return _translationService.GetByKey(status.name, langKey);
        //}

        
        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<GetAllDT>>))]
        [HttpPost("GetAllDT")]
        public async Task<IActionResult> GetAllDT(DataTableDTOs.SessionDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);
            User user = HttpContext.GetUser<User>();

            (int count, List<GetAllDT> list) data = await _sessionService.GetAllDT(dto, user);
            data.list.ForEach(s => s.status.name = _translationService.GetByKey(s.status.name, langKey));

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }


        [AuthorizeUser]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithDataTable<IEnumerable<GetAllDT>>))]
        [HttpPost("GetUserSessions")]
        public async Task<IActionResult> GetUserSessions(DataTableDTOs.UserSessionDT dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            User user = HttpContext.GetUser<User>();
            (int count, List<UserSession> list) data = await _sessionService.GetUserSessions(dto, user);
            data.list.ForEach(s => s.status.name = _translationService.GetByKey(s.status.name, langKey));

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message, data, dto));
        }


        [AuthorizeUser(UserRole.Admin, UserRole.Instructor)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<IEnumerable<SessionPaticipant>>))]
        [HttpGet("GetSessionParticipants")]
        public async Task<IActionResult> GetSessionParticipants(Guid sessionId)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            IEnumerable<SessionPaticipant> data = await _sessionService.GetSessionParticipants(sessionId);
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }


        
        [AuthorizeUser(UserRole.Admin)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<GetById>))]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            GetById data = await _sessionService.GetById(id);
            data.status.name = _translationService.GetByKey(data.status.name, langKey);

            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message, data));
        }



   
        [AuthorizeUser(UserRole.Admin)]
        [ProducesResponseType(200, Type = typeof(MessageResponseWithObj<List<SessionsDTOs.Responses.Instructor>>))]
        [HttpGet("GetInstructors")]
        public async Task<IActionResult> GetInstructors()
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            List<SessionsDTOs.Responses.Instructor> data = await _sessionService.GetInstructors();
            string message = _translationService.GetByKey(TranslationKeys.SuccessFetch, langKey);
            return Ok(MessageResponseFactory.Create(message, data));
        }




     
        [AuthorizeUser(UserRole.Admin)]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(IFormFile? imageFile, [FromForm] Create dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            IFileProxy? file = imageFile != null ? new AzureFormFileProxy(imageFile) : null;
            await _sessionService.Create(file, dto);

            string message = _translationService.GetByKey(TranslationKeys.Created, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message));
        }

  
        [AuthorizeUser(UserRole.Admin)]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(IFormFile? imageFile, [FromForm] Edit dto)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            IFileProxy? file = imageFile != null ? new AzureFormFileProxy(imageFile) : null;
            await _sessionService.Edit(file, dto);

            string message = _translationService.GetByKey(TranslationKeys.Edited, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message));
        }


      
        [AuthorizeUser(UserRole.Admin)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string langKey = Headers.GetLanguage(Request.Headers);

            await _sessionService.Delete(id);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, nameof(Session));
            return Ok(MessageResponseFactory.Create(message));
        }



        [AuthorizeUser(allowAll = true)]
        [HttpPost("AddParticipants")]
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
        [HttpDelete("DeleteParticipant/{id}")]
        public async Task<IActionResult> DeleteParticipant(Guid id)
        {
            User user = HttpContext.GetUser<User>();

            string langKey = Headers.GetLanguage(Request.Headers);

            await _sessionService.DeleteParticipant(id, user);

            string message = _translationService.GetByKey(TranslationKeys.Deleted, langKey, nameof(Participant));
            return Ok(MessageResponseFactory.Create(message));
        }


        [AuthorizeUser(allowAll = true)]
        [HttpDelete("DeleteUserSessionParticipants/{sessionId}")]
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
