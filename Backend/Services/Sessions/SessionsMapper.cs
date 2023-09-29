using AutoMapper;
using Infrastructure.DTOs.Sessions;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.Sessions;
using Infrastructure.Entities.Users;
using static Infrastructure.Enums.Enums;

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



            CreateMap<SessionStatus, SessionsDTOs.Responses.SessionStatusDTO>()
                .ForMember(s => s.value, opt => opt.MapFrom(s => s))
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.ToString()));

            CreateMap<Session, SessionsDTOs.Responses.GetById>();
            CreateMap<Location, SessionsDTOs.Responses.GetById.Location>();

            CreateMap<Instructor, SessionsDTOs.Responses.GetById.Instructor>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));

            CreateMap<Participant, SessionsDTOs.Responses.GetById.Participant>()
                .ForMember(s => s.participantName, opt => opt.MapFrom(s => s.Child.Name));

            CreateMap<User, SessionsDTOs.Responses.GetById.Participant.User>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));


            CreateMap<User, SessionsDTOs.Responses.Instructor>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));

            CreateMap<Session, SessionsDTOs.Responses.GetAllDT>()
                .ForMember(s => s.isParticipating, opt => opt.Ignore())
                .ForMember(s => s.instructors, opt => opt.MapFrom(s => s.Instructors.Select(s => new SessionsDTOs.Responses.GetAllDT.Instructor { id = s.User.Id, name = s.User.FirstName + " " + s.User.LastName }).ToList()))
                .ForMember(s => s.children, opt => opt.MapFrom(s => s.Participants.Select(s => new SessionsDTOs.Responses.GetAllDT.Child { id = s.ChildId, name = s.Child.Name }).ToList()));


            CreateMap<Session, SessionsDTOs.Responses.UserSession>()
                .ForMember(s => s.children, opt => opt.MapFrom(s => s.Participants.Select(s => new SessionsDTOs.Responses.UserSession.Child { id = s.ChildId, name = s.Child.Name }).ToList()))
                .ForMember(s => s.instructors, opt => opt.MapFrom(s => s.Instructors.Select(s => s.User.FirstName + " " + s.User.LastName).ToList()));


            CreateMap<Participant, SessionsDTOs.Responses.SessionPaticipant.Child>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.Child.Name))
                .ForMember(s => s.id, opt => opt.MapFrom(s => s.Child.Id));

            CreateMap<User, SessionsDTOs.Responses.SessionPaticipant.User>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}
