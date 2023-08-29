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
            CreateMap<UserDTOs.Requsts.Create, User>();
            CreateMap<UserDTOs.Requsts.Edit, User>();

            CreateMap<User, UserDTOs.Responses.GetById>();
            CreateMap<User, UserDTOs.Responses.GetAllDT>();
        }
    }
}
