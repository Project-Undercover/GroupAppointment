using AutoMapper;
using Core.IPersistence;
using Core.IServices.Sessions;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Sessions;
using Infrastructure.Exceptions;

namespace Services.Appointments
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




        public async Task<(int count, List<SessionsDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.SessionDT dto)
        {
            ISpecification<Session> specification = _unitOfWork.Repository<Session>().QuerySpecification;


            if (dto.CustomSearch != null)
            {
                var (userId, participantName, from, to) = dto.CustomSearch;

                if (userId.HasValue)
                    specification.Include(nameof(Session.Participants)).Where(s => s.Participants.Any(s => s.UserId == dto.CustomSearch.userId));

                if (!string.IsNullOrEmpty(participantName))
                    specification.Include(nameof(Session.Participants)).Where(s => s.Participants.Any(s => (s.FirstName + " " + s.LastName).Contains(participantName) || (s.User.FirstName + " " + s.User.LastName).Contains(participantName)));

                if (from.HasValue)
                    specification.Where(s => s.StartDate >= from);

                if (to.HasValue)
                    specification.Where(s => s.EndDate <= to);
            }


            var (count, data) = await _unitOfWork.Repository<Session>().Find(specification);
            var mappedData = _mapper.Map<List<SessionsDTOs.Responses.GetAllDT>>(data);
            return (count, mappedData);
        }


        public async Task<SessionsDTOs.Responses.GetById> GetById(Guid id)
        {
            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(id, nameof(Session.Participants));
            return _mapper.Map<SessionsDTOs.Responses.GetById>(session);
        }

        public async Task Create(SessionsDTOs.Requests.Create dto)
        {
            if (dto.endDate <= dto.startDate)
                throw new ValidationException("EndDateMustBeBiggerThanStartDate");

            if ((dto.endDate - dto.startDate).TotalMinutes < 5)
                throw new ValidationException("InvalidSessionDuration");

            bool sessionOverLap = await _unitOfWork.Repository<Session>().Exists(s => s.StartDate <= dto.startDate && s.EndDate >= dto.startDate);
            if (sessionOverLap)
                throw new ValidationException("SessionsOverlap");


            Session session = _mapper.Map<Session>(dto);

            await _unitOfWork.Repository<Session>().AddAsync(session);
            await _unitOfWork.Commit();
        }

        public async Task Edit(SessionsDTOs.Requests.Edit dto)
        {
            Session appointment = await _unitOfWork.Repository<Session>().GetByIdAsync(dto.id);

            _mapper.Map(dto, appointment);

            await _unitOfWork.Repository<Session>().AddAsync(appointment);
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





        public async Task AddParticipant(SessionsDTOs.Requests.AddParticipant dto)
        {
            Participant participant = _mapper.Map<Participant>(dto);

            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(dto.SessionId);
            if (session.ParticipantsCount == session.MaxParticipants)
                throw new ValidationException("SessionIsFull");
            session.ParticipantsCount++;

            await _unitOfWork.Repository<Session>().UpdateAsync(session);
            await _unitOfWork.Repository<Participant>().AddAsync(participant);
            await _unitOfWork.Commit();
        }



        public async Task DeleteParticipant(Guid participantId)
        {
            Participant participant = await _unitOfWork.Repository<Participant>().GetByIdAsync(participantId, includes: new string[] { nameof(Participant.User), nameof(Participant.Session) });

            Session session = await _unitOfWork.Repository<Session>().GetByIdAsync(participant.SessionId);
            session.ParticipantsCount--;

            await _unitOfWork.Repository<Session>().UpdateAsync(session);
            await _unitOfWork.Repository<Participant>().DeleteAsync(participant);
            await _unitOfWork.Commit();


            Console.WriteLine($"{participant.User.FirstName} {participant.User.LastName} has been removed from appointment {participant.Session.StartDate}-{participant.Session.EndDate}");
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
