using AutoMapper;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Users;

namespace Services.Sessions
{
    public class SessionsMapper : Profile
    {
        public SessionsMapper()
        {
            CreateMappers();
        }


        public void CreateMappers()
        {
            CreateMap<SessionsDTOs.Requests.Create.Location, Location>();
            CreateMap<SessionsDTOs.Requests.Create, Session>()
                .ForMember(s => s.Instructors, opt => opt.MapFrom(s => s.instructors.Select(s => new Instructor { UserId = s }).ToList()));
            CreateMap<SessionsDTOs.Requests.Edit, Session>()
                .ForMember(s => s.Instructors, opt => opt.MapFrom(s => s.instructors.Select(s => new Instructor { UserId = s }).ToList()));
            CreateMap<SessionsDTOs.Requests.AddParticipant, Participant>();


            CreateMap<Session, SessionsDTOs.Responses.GetById>();
            CreateMap<Location, SessionsDTOs.Responses.GetById.Location>();

            CreateMap<Instructor, SessionsDTOs.Responses.GetById.Instructor>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));

            CreateMap<Participant, SessionsDTOs.Responses.GetById.Participant>()
                .ForMember(s => s.participantName, opt => opt.MapFrom(s => s.Child.Name));
            CreateMap<User, SessionsDTOs.Responses.GetById.Participant.User>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));



            CreateMap<Session, SessionsDTOs.Responses.GetAllDT>();
        }
    }
}
