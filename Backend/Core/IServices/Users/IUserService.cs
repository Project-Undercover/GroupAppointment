using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;

namespace Core.IServices.Users
{
    public interface IUserService
    {

        Task<(int count, IEnumerable<UserDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.UsersDT dto);
        Task<UserDTOs.Responses.GetById> GetById(Guid id);
        Task Create(UserDTOs.Requsts.Create dto);
        Task Edit(UserDTOs.Requsts.Edit dto);
        Task Delete(Guid id);

        Task<UserDTOs.Responses.BaseLogin> Login(UserDTOs.Requsts.Login dto);
        Task<UserDTOs.Responses.VerifyCode?> VerifyCode(Guid requestId, string code);
        Task<Guid> SendVerificationAgain(UserDTOs.Requsts.SendVerificationCodeAgain dto);


        Task<UserDTOs.Responses.SendVerification> SendVerificationRequest(UserDTOs.Requsts.SendVerification dto);
        Task SetPassword(UserDTOs.Requsts.SetPassword dto);
    }
}
