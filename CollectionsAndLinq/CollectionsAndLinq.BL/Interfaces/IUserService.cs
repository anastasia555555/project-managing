using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> CreateUser(NewUserDto user);
        Task<UserDto> UpdateUser(NewUserDto user);
        Task<UserDto> DeleteUser(int id);
        Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync();
        Task<UserInfoDto> GetUserInfoAsync(int userId);
    }
}
