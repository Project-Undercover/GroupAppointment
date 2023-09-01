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
            CreateMap<UserDTOs.Requests.Create.Child, Child>();

            CreateMap<UserDTOs.Requests.Edit, User>();


            CreateMap<UserDTOs.Requests.AddChild, Child>();


            CreateMap<User, UserDTOs.Responses.GetAllDT>()
                .ForMember(s => s.roleName, opt => opt.MapFrom(s => s.Role));
            CreateMap<User, UserDTOs.Responses.GetById>();
            CreateMap<Child, UserDTOs.Responses.GetById.Child>();
        }
    }
}
