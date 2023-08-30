using static Infrastructure.Enums.Verifications;

namespace Infrastructure.DTOs.Users
{
    public class UserDTOs
    {
        public class Requests
        {

            public record Create 
            {
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string Email { get; set; } = "";
                public string mobileNumber { get; set; }
                public string Password { get; set; }
            }

            public record Edit(Guid Id, bool isActive) : Create;

            public record Login(string username, string password);

            public record SendVerification(string username, VerificationType type);
            public record SendVerificationCodeAgain(Guid requestId);
            public record VerifiyCode(string code, Guid requestId);
            public record SetPassword(string password, Guid requestId, string code);
        }



        public class Responses
        {
            public record BaseLogin { }
            public record Login2FA : BaseLogin { public Guid verificationId { get; set; } }
            public record Login : BaseLogin
            {
                public Guid userId { get; set; }
                public DateTime expiresAt { get; set; }
                public string token { get; set; }
            }

            public record SendVerification(Guid verificationId);


            public record GetAllDT : EntityDTO
            {
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string email { get; set; } = "";
                public string mobileNumber { get; set; }
                public bool isActive { get; set; } = true;
            }


            public record GetById : EntityDTO
            {
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string email { get; set; } = "";
                public string mobileNumber { get; set; }
                public bool isActive { get; set; } = true;
            }




            public record VerifyCode
            {
                public Guid userId { get; set; }
                public string jwt { get; set; }
                public DateTime expiresAt { get; set; }
            }

            public record SendVerificationAgain
            {
                public Guid requestId { get; set; }
            }
        }
    }
}
