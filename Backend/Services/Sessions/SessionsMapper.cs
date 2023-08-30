using AutoMapper;
using Infrastructure.DTOs.Sessions;
using Infrastructure.Entities.Sessions;

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
            CreateMap<SessionsDTOs.Requests.Create, Session>();
            CreateMap<SessionsDTOs.Requests.Edit, Session>();
            CreateMap<SessionsDTOs.Requests.AddParticipant, Participant>();




            CreateMap<Session, SessionsDTOs.Responses.GetById>();
            CreateMap<Participant, SessionsDTOs.Responses.GetById.Participant>();

            CreateMap<Session, SessionsDTOs.Responses.GetAllDT>();
        }
    }
}
