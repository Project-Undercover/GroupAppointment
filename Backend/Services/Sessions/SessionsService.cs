using AutoMapper;
using Core.IPersistence;
using Core.IServices.Sessions;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Sessions;

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
            IFilter<Session> filter = _unitOfWork.Repository<Session>().GetFilter;
            if (dto.CustomSearch != null)
            {
                var (userId, participantName, from, to) = dto.CustomSearch;

                if (userId.HasValue)
                    filter.Include(nameof(Session.Participants)).Add(s => s.Participants.Any(s => s.UserId == dto.CustomSearch.userId));

                if (!string.IsNullOrEmpty(participantName))
                    filter.Include(nameof(Session.Participants)).Add(s => s.Participants.Any(s => (s.FirstName + " " + s.LastName).Contains(participantName) || (s.User.FirstName + " " + s.User.LastName).Contains(participantName)));

                if (from.HasValue)
                    filter.Add(s => s.StartDate >= from);

                if (to.HasValue)
                    filter.Add(s => s.EndDate < to);
            }


            var (count, data) = await _unitOfWork.Repository<Session>().Find(filter: filter);
            var mappedData = _mapper.Map<List<SessionsDTOs.Responses.GetAllDT>>(data);
            return (count, mappedData);
        }


        public async Task<SessionsDTOs.Responses.GetById> GetById(Guid id)
        {
            Session appointment = await _unitOfWork.Repository<Session>().GetByIdAsync(id);
            return _mapper.Map<SessionsDTOs.Responses.GetById>(appointment);
        }

        public async Task Create(SessionsDTOs.Requests.Create dto)
        {
            Session appointment = _mapper.Map<Session>(dto);

            await _unitOfWork.Repository<Session>().AddAsync(appointment);
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
            Session appointment = await _unitOfWork.Repository<Session>().GetByIdAsync(id);

            // if appointment has participants, Send a cancel appointment message
            if (appointment.ParticipantsCount > 0)
                await SendParticipantsMessage(appointment.Id, "Appointment has been canceled");


            await _unitOfWork.Repository<Session>().DeleteAsync(appointment);
            await _unitOfWork.Commit();
        }





        public async Task AddParticipant(SessionsDTOs.Requests.AddParticipant dto)
        {
            Participant participant = _mapper.Map<Participant>(dto);
            await _unitOfWork.Repository<Participant>().AddAsync(participant);
            await _unitOfWork.Commit();
        }



        public async Task DeleteParticipant(Guid participantId)
        {
            Participant participant = await _unitOfWork.Repository<Participant>().GetByIdAsync(participantId, includes: new string[] { nameof(Participant.User), nameof(Participant.Appointment) });

            await _unitOfWork.Repository<Participant>().DeleteAsync(participant);
            await _unitOfWork.Commit();


            Console.WriteLine($"{participant.User.FirstName} {participant.User.LastName} has been removed from appointment {participant.Appointment.StartDate}-{participant.Appointment.EndDate}");
        }






        private async Task SendParticipantsMessage(Guid appointmentId, string message)
        {
            var (count, participants) = await _unitOfWork.Repository<Participant>().Find(s => s.AppointmentId == appointmentId, includes: nameof(Participant.User));

            foreach (var participant in participants)
            {
                Console.WriteLine(message);
            }
        }

    }
}
