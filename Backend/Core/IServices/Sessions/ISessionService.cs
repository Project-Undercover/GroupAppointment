using Core.IUtils;
using Infrastructure.DTOs.DataTables;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.Users;

namespace Core.IServices.Sessions
{
    public interface ISessionService
    {
        Task<(int count, List<SessionsDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.SessionDT dto, User user);
        Task<(int count, List<SessionsDTOs.Responses.UserSession> data)> GetUserSessions(DataTableDTOs.UserSessionDT dto, User user);
        Task<List<SessionsDTOs.Responses.SessionPaticipant>> GetSessionParticipants(Guid sessionId);
        Task<SessionsDTOs.Responses.GetById> GetById(Guid id);
        Task<List<SessionsDTOs.Responses.Instructor>> GetInstructors();
        Task Create(IFileProxy? imageFile, SessionsDTOs.Requests.Create dto);
        Task Edit(IFileProxy? imageFile, SessionsDTOs.Requests.Edit dto);
        Task Delete(Guid id);
        Task AddParticipants(SessionsDTOs.Requests.AddParticipant dto);
        Task DeleteParticipant(Guid participantId, User user);
        Task DeleteUserSessionParticipants(Guid sessionId, User user);
    }
}
