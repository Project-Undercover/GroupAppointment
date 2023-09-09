using Infrastructure.DTOs.Sessions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static Infrastructure.Enums.Enums;
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

                [EmailAddress(ErrorMessage = "Invalid Email Address")]
                [DataType(DataType.EmailAddress)]
                public string? Email { get; set; }

                [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
                [DataType(DataType.PhoneNumber)]
                public string mobileNumber { get; set; }
                public UserRole role { get; set; }
                public virtual List<string> Children { get; set; }
            }

            public record Edit : Create
            {
                public Guid Id { get; set; }
                public bool isActive { get; set; }
                public new List<Child> Children { get; set; }


                public record Child
                {
                    public Guid? id { get; set; }
                    public string name { get; set; }
                    public bool isActive { get; set; }
                }
            }


            public record Login(string username);

            public record SendVerification(string username, VerificationType type);
            public record SendVerificationCodeAgain(Guid requestId);
            public record VerifiyCode(string code, Guid requestId);
            public record SetPassword(string password, Guid requestId, string code);

            public record AddChild(Guid userId, string name);
            public record DeleteChild(Guid userId, Guid childId);

            public record HomeData(DateTime from);

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


            public record HomeData
            {
                public int childrenCount { get; set; }
                public int sessionsCount { get; set; }
                public List<Session> sessions { get; set; }
                public int finishedSessions { get; set; }

                public record Session : EntityDTO
                {
                    public string title { get; set; }
                    public string? image { get; set; }
                    public int ParticipantsCount { get; set; }
                    public int MaxParticipants { get; set; }
                    public DateTimeOffset StartDate { get; set; }
                    public DateTimeOffset EndDate { get; set; }
                    public string? locationName { get; set; }
                    public string instructor { get; set; }
                    public bool isAvailable { get; set; }
                    public List<Child> children { get; set; }

                    public record Child
                    {
                        public Guid id { get; set; }
                        public string name { get; set; }
                    }
                }
            }

            public record GetAllDT : EntityDTO
            {
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string email { get; set; } = "";
                public string mobileNumber { get; set; }
                public string? image { get; set; }
                public bool isActive { get; set; } = true;
                public int role { get; set; }
                public string roleName { get; set; }
            }


            public record GetById : EntityDTO
            {
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string email { get; set; } = "";
                public string mobileNumber { get; set; }
                public string? image { get; set; }
                public int role { get; set; }
                public bool isActive { get; set; } = true;
                public List<Child> Children { get; set; } = new List<Child>();

                public record Child
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                    public bool isActive { get; set; }
                }
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
