using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;

namespace Core.IServices.Users
{
    public interface IUserService
    {

        Task<(int count, IEnumerable<UserDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.UsersDT dto);
        Task<UserDTOs.Responses.GetById> GetById(Guid id);
        Task Create(UserDTOs.Requests.Create dto);
        Task Edit(UserDTOs.Requests.Edit dto);
        Task Delete(Guid id);


        Task AddChild(UserDTOs.Requests.AddChild dto);
        Task DeleteChild(Guid id);


        Task<UserDTOs.Responses.BaseLogin> Login(UserDTOs.Requests.Login dto);
        Task<UserDTOs.Responses.VerifyCode?> VerifyCode(Guid requestId, string code);
        Task<Guid> SendVerificationAgain(UserDTOs.Requests.SendVerificationCodeAgain dto);


        Task<UserDTOs.Responses.SendVerification> SendVerificationRequest(UserDTOs.Requests.SendVerification dto);
    }
}
