using Infrastructure.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices.Auth
{
    public interface IAuthService
    {
        Task<UserDTOs.Responses.BaseLogin> Login(UserDTOs.Requests.Login dto);
        Task<UserDTOs.Responses.VerifyCode?> VerifyCode(Guid requestId, string code);
        Task<Guid> SendVerificationAgain(UserDTOs.Requests.SendVerificationCodeAgain dto);
        Task<UserDTOs.Responses.SendVerification> SendVerificationRequest(UserDTOs.Requests.SendVerification dto);
    }
}
