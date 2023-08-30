using AutoMapper;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.Users;

namespace Services.Users
{
    public class UserServiceMapper : Profile
    {
        public UserServiceMapper()
        {
            CreateMappers();
        }


        public void CreateMappers()
        {
            CreateMap<UserDTOs.Requests.Create, User>();
            CreateMap<UserDTOs.Requests.Edit, User>();

            CreateMap<User, UserDTOs.Responses.GetById>();
            CreateMap<User, UserDTOs.Responses.GetAllDT>();
        }
    }
}
