using Core.IUtils;
using Infrastructure.DTOs.Users;
using Infrastructure.Entities.DataTables;
using Infrastructure.Entities.Users;

namespace Core.IServices.Users
{
    public interface IUserService
    {
        Task<UserDTOs.Responses.HomeData> GetHomeData(UserDTOs.Requests.HomeData dto, User user);
        Task<(int count, IEnumerable<UserDTOs.Responses.GetAllDT> data)> GetAllDT(DataTableDTOs.UsersDT dto, User user);
        Task<UserDTOs.Responses.GetById> GetById(Guid id);
        Task Create(UserDTOs.Requests.Create dto);
        Task Edit(UserDTOs.Requests.Edit dto);
        Task Delete(Guid id);


        Task AddChild(UserDTOs.Requests.AddChild dto);
        Task DeleteChild(Guid id);
        Task EditProfileImage(Guid userId, IFileProxy imageFile);
    }
}
