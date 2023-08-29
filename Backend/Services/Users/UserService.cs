using AutoMapper;
using Core.IPersistence;
using Core.IServices.Users;
using Core.IUtils;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.Others;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Users;
using Infrastructure.Exceptions;
using Infrastructure.Utils;
using static Infrastructure.Enums.Verifications;

namespace Services.Users
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHash _passwordHash;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper,
            IEmailService emailService,
            ITokenGenerator tokenGenerator,
            IPasswordHash passwordHash)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _tokenGenerator = tokenGenerator;
            _passwordHash = passwordHash;
        }




        public async Task<(int count, IEnumerable<UserDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.UsersDT dto)
        {
            var filters = _unitOfWork.Repository<User>().GetFilterBuilder;

            if (dto.CustomSearch != null)
            {
                if (!string.IsNullOrEmpty(dto.CustomSearch.name))
                    filters.Add(user => (user.FirstName + " " + user.LastName).Contains(dto.CustomSearch.name));

                if (dto.CustomSearch.isActive.HasValue)
                    filters.Add(user => user.IsActive);

                if (!string.IsNullOrEmpty(dto.CustomSearch.mobileNumber))
                    filters.Add(user => user.MobileNumber.Contains(dto.CustomSearch.mobileNumber));
            }

            (int count, IEnumerable<User> users) data = await _unitOfWork
                .Repository<User>()
                .GetAsync(filters.Build(), skip: dto.skip, take: dto.take);

            return (data.count, data.users
                .Select(user => _mapper.Map<UserDTOs.Responses.GetAllDT>(user))
                .ToList());
        }
        public async Task<UserDTOs.Responses.GetById> GetById(Guid id)
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            return _mapper.Map<UserDTOs.Responses.GetById>(user);
        }
        public async Task Create(UserDTOs.Requsts.Create dto)
        {

            bool emailExists = await _unitOfWork.Repository<User>().Exists(s => s.Email == dto.Email);
            if (emailExists)
                throw new ValidationException("AlreadyExists", "Email");

            bool mobileNumberExists = await _unitOfWork.Repository<User>().Exists(s => s.MobileNumber == dto.mobileNumber);
            if (mobileNumberExists)
                throw new ValidationException("AlreadyExists", "MobileNumber");



            //User user = new User(signup.FirstName, signup.LastName, signup.Email, signup.Password ,signup.MobileNumber, company);
            User user = _mapper.Map<User>(dto);
            user.Password = _passwordHash.Hash(user.Password);

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.Commit();
        }
        public async Task Edit(UserDTOs.Requsts.Edit dto)
        {
            User? user = await _unitOfWork.Repository<User>().GetByIdAsync(dto.Id);
            if (user is null)
                throw new NotFoundException("UserNotFound");

            bool emailExists = await _unitOfWork.Repository<User>().Exists(s => s.Email == dto.Email && s.Id != user.Id);
            if (emailExists)
                throw new ValidationException("EmailExists");

            bool mobileNumberExists = await _unitOfWork.Repository<User>().Exists(s => s.MobileNumber == dto.Email);
            if (mobileNumberExists)
                throw new ValidationException("MobileExists");

            user = _mapper.Map(dto, user);
            user.Password = _passwordHash.Hash(user.Password);


            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (!user.IsActive) throw new ValidationException("AlreadyDeleted", "User");

            user.IsActive = false;

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
        }








        public async Task<UserDTOs.Responses.BaseLogin> Login(UserDTOs.Requsts.Login dto)
        {
            User user = await _unitOfWork.Repository<User>().GetByAsync(s => s.Email == dto.username || s.MobileNumber == dto.username);

            if (!user.IsActive)
                throw new ForbiddenException("NotActive", "User");

            bool isValidPassword = _passwordHash.VerifyHash(dto.password, user.Password);
            if (!isValidPassword)
                throw new ValidationException("LoginNotValid");


            (string token, DateTime expiresAt) = _tokenGenerator.GenerateToken(user.Id);


            if (!Constants.TwoFactorAuthEnabled)
                return new UserDTOs.Responses.Login { userId = user.Id, expiresAt = expiresAt, token = token };

            Guid verificationId = await CreateVerificationRequest(user, VerificationType.Login);
            return new UserDTOs.Responses.Login2FA { verificationId = verificationId };
        }
        public async Task<UserDTOs.Responses.SendVerification> SendVerificationRequest(UserDTOs.Requsts.SendVerification dto)
        {
            User user = await _unitOfWork.Repository<User>().GetByAsync(s => s.Email == dto.username);
            Guid verificationId = await CreateVerificationRequest(user, dto.type);
            return new UserDTOs.Responses.SendVerification(verificationId);
        }
        public async Task SetPassword(UserDTOs.Requsts.SetPassword dto)
        {
            VerificationRequest request = await _unitOfWork.VerificationRequests.GetByIdAsync(dto.requestId);
            await VerifyRequestCode(request, dto.code);
            User user = await _unitOfWork.Repository<User>().GetByAsync(s => s.Id == request.UserId);

            if (!user.IsActive)
                throw new ForbiddenException("NotActive", "User");

            user.Password = _passwordHash.Hash(dto.password);

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
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
        public async Task<Guid> SendVerificationAgain(UserDTOs.Requsts.SendVerificationCodeAgain dto)
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
            IEmailService.IEmailMessageBuilder messageBuilder = _emailService.GetMessageBuilder();
            string msg = messageBuilder
                .AddMessage("Your verification code:")
                .AddMessage(code, isBold: true)
                .AddMessage("Expires after: ", withBreak: false)
                .AddMessage($"{Constants.CodeExpirationTimeInMin}min")
                .Build();

            _emailService.SendEmail("SuperApp Verification Request", msg, new List<string> { user.Email });

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
