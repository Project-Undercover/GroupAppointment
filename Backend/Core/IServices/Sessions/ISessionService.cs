using Core.IUtils;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.DataTables;

namespace Core.IServices.Sessions
{
    public interface ISessionService
    {
        Task<(int count, List<SessionsDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.SessionDT dto);
        Task<SessionsDTOs.Responses.GetById> GetById(Guid id);
        Task Create(IFileProxy? imageFile, SessionsDTOs.Requests.Create dto);
        Task Edit(IFileProxy? imageFile, SessionsDTOs.Requests.Edit dto);
        Task Delete(Guid id);
        Task AddParticipant(SessionsDTOs.Requests.AddParticipant dto);
        Task DeleteParticipant(Guid participantId);
    }
}
