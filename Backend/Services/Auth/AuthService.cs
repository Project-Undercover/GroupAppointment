using AutoMapper;
using Core.IPersistence;
using Core.IServices.Auth;
using Core.IUtils;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.Others;
using Infrastructure.Entities.Users;
using Infrastructure.Exceptions;
using Infrastructure.Utils;
using static Infrastructure.Enums.Verifications;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenGenerator _tokenGenerator;


        public AuthService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService,
            ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _tokenGenerator = tokenGenerator;
        }



        public async Task<UserDTOs.Responses.BaseLogin> Login(UserDTOs.Requests.Login dto)
        {
            User user = await _unitOfWork.Repository<User>().GetByAsync(s => s.Email == dto.username || s.MobileNumber == dto.username);

            if (!user.IsActive)
                throw new ForbiddenException("NotActive", "User");


            (string token, DateTime expiresAt) = _tokenGenerator.GenerateToken(user.Id);


            if (!Constants.TwoFactorAuthEnabled)
                return new UserDTOs.Responses.Login { userId = user.Id, expiresAt = expiresAt, token = token };

            Guid verificationId = await CreateVerificationRequest(user, VerificationType.Login);
            return new UserDTOs.Responses.Login2FA { verificationId = verificationId };
        }
        public async Task<UserDTOs.Responses.SendVerification> SendVerificationRequest(UserDTOs.Requests.SendVerification dto)
        {
            User user = await _unitOfWork.Repository<User>().GetByAsync(s => s.Email == dto.username);
            Guid verificationId = await CreateVerificationRequest(user, dto.type);
            return new UserDTOs.Responses.SendVerification(verificationId);
        }

        /// <summary>
        /// Verifiy the verification request
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="code"></param>
        /// <param name="forAuth">to detirment weither to return a token or not</param>
        /// <returns>token</returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<UserDTOs.Responses.VerifyCode?> VerifyCode(Guid requestId, string code)
        {
            VerificationRequest request = await _unitOfWork.VerificationRequests.GetByAsync(s => s.Id == requestId);

            User user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);

            if (!user.IsActive)
                throw new ValidationException("NotActive", "User");

            await VerifyRequestCode(request, code);

            request.Confirmed = true;

            await _unitOfWork.VerificationRequests.UpdateAsync(request);
            await _unitOfWork.Commit();

            // if its a request that requires a token 
            if (request.Type == VerificationType.Login)
            {
                var tokenData = _tokenGenerator.GenerateToken(user.Id);
                return new UserDTOs.Responses.VerifyCode()
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    jwt = tokenData.token,
                    expiresAt = tokenData.expireAt
                };
            }

            return null;
        }
        /// <summary>
        /// Send a verification code
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>request id</returns>
        /// <exception cref="CustomValidationException"></exception>
        public async Task<Guid> SendVerificationAgain(UserDTOs.Requests.SendVerificationCodeAgain dto)
        {
            VerificationRequest request = await _unitOfWork.VerificationRequests.GetByIdAsync(dto.requestId);
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            return await CreateVerificationRequest(user, request.Type);
        }
        /// <summary>
        /// Create Verification Request and send sms message
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private async Task<Guid> CreateVerificationRequest(User user, VerificationType type)
        {
            // check number of attempts 
            int attempts = await _unitOfWork.VerificationRequests.CountAttemptsAsync(user.Id, Constants.VerificationCodeDuration);
            if (attempts > Constants.VerificationAttempts)
                throw new ValidationException("MaxVerificationAttempts");


            var code = StaticFunctions.GenerateRandomCode();

            VerificationRequest request = new VerificationRequest
            {
                Code = code,
                Type = type,
                UserId = user.Id,
                ExpiresAt = DateTime.Now.AddMinutes(Constants.CodeExpirationTimeInMin),
            };
            await _unitOfWork.VerificationRequests.AddAsync(request);

            // send sms
            //IEmailService.IEmailMessageBuilder messageBuilder = _emailService.GetMessageBuilder();
            //string msg = messageBuilder
            //    .AddMessage("Your verification code:")
            //    .AddMessage(code, isBold: true)
            //    .AddMessage("Expires after: ", withBreak: false)
            //    .AddMessage($"{Constants.CodeExpirationTimeInMin}min")
            //    .Build();

            //_emailService.SendEmail("SuperApp Verification Request", msg, new List<string> { user.Email });

            await _unitOfWork.Commit();
            return request.Id;
        }
        private Task VerifyRequestCode(VerificationRequest request, string code)
        {
            if (request.Confirmed)
                throw new ValidationException("VerificationAlreadyConfirmed");

            if (DateTime.Now > request.ExpiresAt)
                throw new ValidationException("Verification expired");

            if (request.Code != code)
                throw new ValidationException("InvalidVerificationCode");

            return Task.CompletedTask;
        }

    }
}
