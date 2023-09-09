using AutoMapper;
using Core.IPersistence;
using Core.IServices.Users;
using Core.IUtils;
using Infrastructure.DTOs.Sessions;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Users;
using Infrastructure.Exceptions;
using System.Runtime.InteropServices;

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



        public async Task<UserDTOs.Responses.HomeData> GetHomeData(UserDTOs.Requests.HomeData dto, User user)
        {
            int childrenCount = await _unitOfWork.Repository<Child>().CountAsync(s => s.UserId == user.Id);
            int finishedSessions = await _unitOfWork.Repository<Session>().CountAsync(s => s.Participants.Any(s => s.UserId == user.Id));

            ISpecification<Session> specification = _unitOfWork.Repository<Session>().QuerySpecification;
            specification
                .Include(nameof(Session.Participants))
                .Include(nameof(Session.Participants) + "." + nameof(Participant.Child))
                .Include(nameof(Session.Instructors))
                .Include(nameof(Session.Instructors) + "." + nameof(Instructor.User))
                .Where(s => s.Participants.Any(s => s.UserId == user.Id))
                .Where(s => s.StartDate >= dto.from)
                .ApplyOrderings(s => s.OrderBy(e => e.StartDate))
                .SkipAndTake(0, 10);

            var (count, data) = await _unitOfWork.Repository<Session>().Find(specification);
            data.ToList().ForEach(s => s.Participants = s.Participants.Where(s => s.UserId == user.Id).ToList()); // only user children 
            var mappedData = _mapper.Map<List<UserDTOs.Responses.HomeData.Session>>(data);

            var homeData = new UserDTOs.Responses.HomeData { childrenCount = childrenCount, finishedSessions = finishedSessions, sessionsCount = count, sessions = mappedData };
            return homeData;
        }

        public async Task<(int count, IEnumerable<UserDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.UsersDT dto, User user)
        {
            var specification = _unitOfWork.Repository<User>().QuerySpecification;

            if (dto.CustomSearch != null)
            {
                if (dto.CustomSearch.withCurrentUser.HasValue && !dto.CustomSearch.withCurrentUser.Value)
                    specification.Where(s => s.Id != user.Id);

                if (!string.IsNullOrEmpty(dto.CustomSearch.name))
                    specification.Where(user => (user.FirstName + " " + user.LastName).Contains(dto.CustomSearch.name));

                if (dto.CustomSearch.isActive.HasValue)
                    specification.Where(user => user.IsActive);

                if (!string.IsNullOrEmpty(dto.CustomSearch.mobileNumber))
                    specification.Where(user => user.MobileNumber.Contains(dto.CustomSearch.mobileNumber));

                if (dto.CustomSearch.roles != null)
                    specification.Where(user => dto.CustomSearch.roles.Contains(user.Role));
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

        public async Task EditProfileImage(Guid userId, IFileProxy imageFile)
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
            user.Image = await imageFile.SaveFile(user.Id.ToString()) + $"?updated={DateTime.Now.Ticks}";
            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
        }
    }
}
