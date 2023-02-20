using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync();
        Task<UserInfoDto> GetUserInfoAsync(int userId);
    }
}
