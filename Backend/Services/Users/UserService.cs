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
using Infrastructure.Entities.Sessions;

namespace Services.Users
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenGenerator _tokenGenerator;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper,
            IEmailService emailService,
            ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _tokenGenerator = tokenGenerator;
        }




        public async Task<(int count, IEnumerable<UserDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.UsersDT dto)
        {
            var specification = _unitOfWork.Repository<User>().QuerySpecification;


            if (dto.CustomSearch != null)
            {
                if (!string.IsNullOrEmpty(dto.CustomSearch.name))
                    specification.Where(user => (user.FirstName + " " + user.LastName).Contains(dto.CustomSearch.name));

                if (dto.CustomSearch.isActive.HasValue)
                    specification.Where(user => user.IsActive);

                if (!string.IsNullOrEmpty(dto.CustomSearch.mobileNumber))
                    specification.Where(user => user.MobileNumber.Contains(dto.CustomSearch.mobileNumber));
            }

            specification
                .ApplyOrderings(s => s.OrderByDescending(s => s.IsActive).ThenByDescending(s => s.CreatedAt))
                .SkipAndTake(dto.skip, dto.take);

            var (count, data) = await _unitOfWork.Repository<User>().Find(specification);

            var users = data
                .Select(_mapper.Map<UserDTOs.Responses.GetAllDT>)
                .ToList();


            return (count, users);
        }
        public async Task<UserDTOs.Responses.GetById> GetById(Guid id)
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id, nameof(User.Children));
            return _mapper.Map<UserDTOs.Responses.GetById>(user);
        }
        public async Task Create(UserDTOs.Requests.Create dto)
        {

            if (!string.IsNullOrEmpty(dto.Email))
            {
                bool emailExists = await _unitOfWork.Repository<User>().Exists(s => s.Email == dto.Email);
                if (emailExists)
                    throw new ValidationException("AlreadyExists", "Email");
            }

            bool mobileNumberExists = await _unitOfWork.Repository<User>().Exists(s => s.MobileNumber == dto.mobileNumber);
            if (mobileNumberExists)
                throw new ValidationException("AlreadyExists", "MobileNumber");

            User user = _mapper.Map<User>(dto);
            user.IsAdmin = user.Role == Infrastructure.Enums.Enums.UserRole.Admin;
            user.ChildrenNumber = user.Children.Count();

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.Commit();
        }
        public async Task Edit(UserDTOs.Requests.Edit dto)
        {
            User? user = await _unitOfWork.Repository<User>().GetByIdAsync(dto.Id, nameof(User.Children));
            if (user is null)
                throw new NotFoundException("NotFound", nameof(User));

            bool emailExists = await _unitOfWork.Repository<User>().Exists(s => s.Email == dto.Email && s.Id != user.Id);
            if (emailExists)
                throw new ValidationException("AlreadyExists", nameof(User.Email));

            bool mobileNumberExists = await _unitOfWork.Repository<User>().Exists(s => s.MobileNumber == dto.Email && s.Id != user.Id);
            if (mobileNumberExists)
                throw new ValidationException("AlreadyExists", nameof(User.MobileNumber));

            List<Guid> checkIfChildrenExists = dto.Children.Where(s => s.id.HasValue).Select(s => s.id.Value).ToList();
            foreach (var id in checkIfChildrenExists)
            {
                bool exsits = await _unitOfWork.Repository<Child>().Exists(s => s.Id == id && s.UserId == user.Id);
                if (!exsits)
                    throw new ValidationException("NotFound", nameof(Child));
            }


            _mapper.Map(dto, user);
            user.IsAdmin = user.Role == Infrastructure.Enums.Enums.UserRole.Admin;
            user.ChildrenNumber = user.Children.Count();

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id);

            bool canDelete = await _unitOfWork.Repository<Participant>().Exists(s => s.UserId == id);
            if (canDelete)
            {   // hard delete
                await _unitOfWork.Repository<User>().DeleteAsync(user);
                return;
            }

            // soft delete
            if (!user.IsActive) throw new ValidationException("AlreadyDeleted", "User");
            user.IsActive = false;

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
        }


        public async Task AddChild(UserDTOs.Requests.AddChild dto)
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(dto.userId);
            user.ChildrenNumber++;

            Child child = _mapper.Map<Child>(dto);

            await _unitOfWork.Repository<Child>().AddAsync(child);
            await _unitOfWork.Commit();
        }
        public async Task DeleteChild(Guid id)
        {
            Child child = await _unitOfWork.Repository<Child>().GetByIdAsync(id);

            User user = await _unitOfWork.Repository<User>().GetByIdAsync(child.UserId);
            user.ChildrenNumber--;

            await _unitOfWork.Repository<Child>().DeleteAsync(child);
            await _unitOfWork.Commit();
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
