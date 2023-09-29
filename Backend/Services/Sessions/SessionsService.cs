using AutoMapper;
using Core.IPersistence;
using Core.IServices.Sessions;
using Core.IUtils;
using Infrastructure.DTOs.DataTables;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Users;
using Infrastructure.Enums;
using Infrastructure.Exceptions;
using Infrastructure.Utils;

namespace Services.Sessions
{
    public class SessionsService : ISessionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<(int count, List<SessionsDTOs.Responses.UserSession> data)> GetUserSessions(DataTableDTOs.UserSessionDT dto, User user)
        {
            ISpecification<Session> specification = _unitOfWork.Repository<Session>().QuerySpecification.Where(s => s.Participants.Any(s => s.UserId == user.Id));


            if (dto.CustomSearch != null)
            {
                var (from, to, searchTerm) = dto.CustomSearch;


                if (!string.IsNullOrEmpty(searchTerm))
                    specification.Where(s =>
                            s.Title.Contains(dto.CustomSearch.searchTerm)
                            || s.Participants.Any(s => s.Child.Name.Contains(searchTerm) || (s.User.FirstName + " " + s.User.LastName).Contains(searchTerm)));

                if (from.HasValue)
                    specification.Where(s => s.StartDate >= from);

                if (to.HasValue)
                    specification.Where(s => s.EndDate <= to);
            }

            specification
                .Include(nameof(Session.Instructors))
                .Include(nameof(Session.Instructors) + "." + nameof(Instructor.User))
                .Include(nameof(Session.Participants))
                .Include(nameof(Session.Participants) + "." + nameof(Participant.Child))
                .ApplyOrderings(s => s.OrderBy(s => s.StartDate))
                .SkipAndTake(dto.skip, dto.take);

            var (count, data) = await _unitOfWork.Repository<Session>().Find(specification);
            data = data.ToList().Select(s =>
            {
                s.Participants = s.Participants.Where(s => s.UserId == user.Id).ToList();
                return s;
            }).ToList(); // only user children 

            var mappedData = _mapper.Map<List<SessionsDTOs.Responses.UserSession>>(data);
            return (count, mappedData);
        }
        public async Task<(int count, List<SessionsDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.SessionDT dto, User user)
        {
            ISpecification<Session> specification = _unitOfWork.Repository<Session>().QuerySpecification;


            if (dto.CustomSearch != null)
            {
                var (searchTerm, from, to) = dto.CustomSearch;

                if (!string.IsNullOrEmpty(searchTerm))
                    specification
                        .Where(s =>
                            s.Title.Contains(dto.CustomSearch.searchTerm)
                            || s.Participants.Any(s => s.Child.Name.Contains(searchTerm) || (s.User.FirstName + " " + s.User.LastName).Contains(searchTerm)));

                if (from.HasValue)
                    specification.Where(s => s.StartDate >= from);

                if (to.HasValue)
                    specification.Where(s => s.EndDate <= to);

                if (!user.IsAdmin)
                    specification.Where(s => s.isVisible);
            }

            specification
                .Include(nameof(Session.Participants))
                .Include(nameof(Session.Participants) + "." + nameof(Participant.User))
                .Include(nameof(Session.Participants) + "." + nameof(Participant.Child))
                .Include(nameof(Session.Instructors))
                .Include(nameof(Session.Instructors) + "." + nameof(Instructor.User))
                .ApplyOrderings(s => s.OrderBy(s => s.StartDate))
                .SkipAndTake(dto.skip, dto.take);

            var (count, data) = await _unitOfWork.Repository<Session>().Find(specification);
            data = data.ToList().Select(s =>
            {
                s.Participants = s.Participants.Where(s => s.UserId == user.Id).ToList();
                return s;
            }).ToList(); // only user children 

            var mappedData = _mapper.Map<List<SessionsDTOs.Responses.GetAllDT>>(data);
            return (count, mappedData);
        }
        public async Task<SessionsDTOs.Responses.GetById> GetById(Guid id)
        {
            Session session = await _unitOfWork
                .Repository<Session>()
                .GetByIdAsync(id,
                nameof(Session.Instructors),
                nameof(Session.Instructors) + "." + nameof(Instructor.User),
                nameof(Session.Participants),
                nameof(Session.Participants) + "." + nameof(Participant.Child),
                nameof(Session.Participants) + "." + nameof(Participant.User));

            return _mapper.Map<SessionsDTOs.Responses.GetById>(session);
        }


        public async Task<List<SessionsDTOs.Responses.Instructor>> GetInstructors()
        {
            var (count, instructors) = await _unitOfWork
                .Repository<User>()
                .Find(s => s.Role == Enums.UserRole.Admin || s.Role == Enums.UserRole.Instructor);

            return _mapper.Map<List<SessionsDTOs.Responses.Instructor>>(instructors);
        }
        public async Task<List<SessionsDTOs.Responses.SessionPaticipant>> GetSessionParticipants(Guid sessionId)
        {
            var (count, participants) = await _unitOfWork.Repository<Participant>().Find(s => s.SessionId == sessionId,
                includes: new string[] { nameof(Participant.Child), nameof(Participant.User) });

            var data = participants
                .GroupBy(s => s.User)
                .Select(s => new SessionsDTOs.Responses.SessionPaticipant
                {
                    user = _mapper.Map<SessionsDTOs.Responses.SessionPaticipant.User>(s.Key),
                    children = _mapper.Map<List<SessionsDTOs.Responses.SessionPaticipant.Child>>(s.OrderBy(s => s.Child.Name).ToList())
                })
                .OrderBy(s => s.user.name)
                .ToList();

            return data;
        }



        public async Task ValidateSession(SessionsDTOs.Requests.Create dto)
        {
            if (dto is not SessionsDTOs.Requests.Edit)
                if (dto.startDate < DateTime.Now)
                    throw new ValidationException("StartDateCantBeOlderThanNow");

            if (dto.endDate <= dto.startDate)
                throw new ValidationException("EndDateMustBeBiggerThanStartDate");

            if ((dto.endDate - dto.startDate).TotalMinutes < 5)
                throw new ValidationException("InvalidSessionDuration", "5", "Minute");


            bool sessionOverLap;
            if (dto is SessionsDTOs.Requests.Edit)
                sessionOverLap = await _unitOfWork
                    .Repository<Session>()
                    .Exists(s => s.StartDate < dto.endDate && s.EndDate > dto.startDate && s.Id != ((SessionsDTOs.Requests.Edit)dto).id);
            else
                sessionOverLap = await _unitOfWork.Repository<Session>().Exists(s => s.StartDate < dto.endDate && s.EndDate > dto.startDate);

            if (sessionOverLap)
                throw new ValidationException("SessionsOverlap");


            foreach (var instructorId in dto.instructors)
            {
                User instructor = await _unitOfWork.Repository<User>().GetByIdAsync(instructorId);
                if (instructor.Role != Infrastructure.Enums.Enums.UserRole.Admin && instructor.Role != Infrastructure.Enums.Enums.UserRole.Instructor)
                    throw new ValidationException("OnlyAdminAndInstructorCanBeInstructors");
            }
        }
        public async Task Create(IFileProxy? imageFile, SessionsDTOs.Requests.Create dto)
        {
            await ValidateSession(dto);

            Session session = _mapper.Map<Session>(dto);
            session.Id = Guid.NewGuid();

            if (imageFile != null)
            {
                string imageFileName = session.Id.ToString();
                session.Image = await imageFile.SaveFile(imageFileName, Infrastructure.Enums.Enums.Folder.SessionImages) + $"?updated={DateTime.Now.Ticks}";
            }

            await _unitOfWork.Repository<Session>().AddAsync(session);
            await _unitOfWork.Commit();
        }
        public async Task Edit(IFileProxy? imageFile, SessionsDTOs.Requests.Edit dto)
        {
            await ValidateSession(dto);

            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(dto.id, nameof(Session.Instructors));

            if (session.ParticipantsCount > dto.maxParticipants)
                throw new ValidationException("ParticipantsIsBiggerThanMaxParticipants");
            _mapper.Map(dto, session);

            if (imageFile != null)
            {
                string imageFileName = session.Id.ToString();
                session.Image = await imageFile.SaveFile(imageFileName, Infrastructure.Enums.Enums.Folder.SessionImages) + $"?lastupdated={DateTime.Now.Ticks}";
            }


            await _unitOfWork.Repository<Session>().UpdateAsync(session);
            await _unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(id);

            // if appointment has participants, Send a cancel appointment message
            if (session.ParticipantsCount > 0)
                await SendParticipantsMessage(session.Id, "Appointment has been canceled");

            await _unitOfWork.Repository<Session>().DeleteAsync(session);
            await _unitOfWork.Commit();
        }


        public async Task AddParticipants(SessionsDTOs.Requests.AddParticipant dto)
        {
            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(dto.SessionId);
            if (session.ParticipantsCount == session.MaxParticipants)
                throw new ValidationException("SessionIsFull");

            if (session.ParticipantsCount + dto.Children.Count() > session.MaxParticipants)
                throw new ValidationException("SessionNoEnoughRoom");

            if (session.Status != Enums.SessionStatus.Available)
                throw new ValidationException("SessionNotAvailable");

            if (session.StartDate <= DateTime.Now)
                throw new ValidationException("SessionHasBeenStarted");


            List<Participant> participants = new();
            foreach (var childId in dto.Children)
            {
                Child child = await _unitOfWork.Repository<Child>().GetByIdAsync(childId);

                bool alreadyParticiping = await _unitOfWork.Repository<Participant>().Exists(s => s.SessionId == dto.SessionId && s.ChildId == childId);
                if (alreadyParticiping) throw new ValidationException(TranslationKeys.ChildAlreadyParticipating);

                Participant participant = _mapper.Map<Participant>(dto);
                participant.ChildId = childId;
                participant.UserId = child.UserId;
                participants.Add(participant);
            }

            session.ParticipantsCount += dto.Children.Count();

            if (session.ParticipantsCount == session.MaxParticipants)
                session.Status = Enums.SessionStatus.Full;

            await _unitOfWork.Repository<Participant>().AddRangeAsync(participants);
            await _unitOfWork.Repository<Session>().UpdateAsync(session);
            await _unitOfWork.Commit();
        }
        public async Task DeleteParticipant(Guid participantId, User user)
        {
            Participant participant = await _unitOfWork.Repository<Participant>().GetByIdAsync(participantId, includes: new string[] { nameof(Participant.User), nameof(Participant.Session) });
            if (!user.IsAdmin && participant.UserId != user.Id) throw new UnAuthorizedException(TranslationKeys.UnAuthorized);


            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(participant.SessionId);

            if (session.Status != Enums.SessionStatus.Available)
                throw new ValidationException("SessionNotAvailable");

            session.ParticipantsCount--;
            session.Status = Enums.SessionStatus.Available;


            await _unitOfWork.Repository<Session>().UpdateAsync(session);
            await _unitOfWork.Repository<Participant>().DeleteAsync(participant);
            await _unitOfWork.Commit();

            if (user.IsAdmin && participant.UserId != user.Id)
                Console.WriteLine($"{participant.User.FirstName} {participant.User.LastName} has been removed from appointment {participant.Session.StartDate}-{participant.Session.EndDate}");
        }
        public async Task DeleteUserSessionParticipants(Guid sessionId, User user)
        {
            var (count, participants) = await _unitOfWork.Repository<Participant>().Find(s => s.UserId == user.Id && s.SessionId == sessionId);
            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(sessionId);
            if (session.Status != Enums.SessionStatus.Available)
                throw new ValidationException("SessionNotAvailable");


            session.ParticipantsCount -= participants.Count();
            session.Status = Enums.SessionStatus.Available;

            await _unitOfWork.Repository<Session>().UpdateAsync(session);
            await _unitOfWork.Repository<Participant>().DeleteRangeAsync(participants);
            await _unitOfWork.Commit();
        }
        private async Task SendParticipantsMessage(Guid appointmentId, string message)
        {
            var (count, participants) = await _unitOfWork.Repository<Participant>().Find(s => s.SessionId == appointmentId, includes: nameof(Participant.User));

            foreach (var participant in participants)
            {
                Console.WriteLine(message);
            }
        }
    }
}
