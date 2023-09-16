using AutoMapper;
using Infrastructure.DTOs.Sessions;
using Infrastructure.DTOs.Users;
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


            CreateMap<User, SessionsDTOs.Responses.Instructor>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));

            CreateMap<Session, SessionsDTOs.Responses.GetAllDT>()
                .ForMember(s => s.instructor, opt => opt.MapFrom(s => s.Instructors.Select(s => s.User.FirstName + " " + s.User.LastName).FirstOrDefault()))
                .ForMember(s => s.children, opt => opt.MapFrom(s => s.Participants.Select(s => new SessionsDTOs.Responses.GetAllDT.Child { id = s.ChildId, name = s.Child.Name }).ToList()));


            CreateMap<Session, SessionsDTOs.Responses.UserSession>()
                .ForMember(s => s.children, opt => opt.MapFrom(s => s.Participants.Select(s => new SessionsDTOs.Responses.UserSession.Child { id = s.ChildId, name = s.Child.Name }).ToList()))
                .ForMember(s => s.instructor, opt => opt.MapFrom(s => s.Instructors.Select(s => s.User.FirstName + " " + s.User.LastName).FirstOrDefault()));


            CreateMap<Participant, SessionsDTOs.Responses.Child>()
                .ForMember(s => s.name, opt => opt.MapFrom(s => s.Child.Name))
                .ForMember(s => s.id, opt => opt.MapFrom(s => s.Child.Id));
        }
    }
}
