using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController(IUserService service) => _service = service;

        [HttpGet]
        public async Task<List<UserDto>> GetUsers()
        {
            return await _service.GetUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUser(int id)
        {
            return await _service.GetUserAsync(id);
        }

        [HttpPost]
        public async Task<UserDto> AddUser([FromBody] NewUserDto user)
        {
            return await _service.CreateUser(user);
        }

        [HttpPut]
        public async Task<UserDto> EditUser([FromBody] NewUserDto user)
        {
            return await _service.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public async Task<UserDto> DeleteUser(int id)
        {
            return await _service.DeleteUser(id);
        }

        [HttpGet("/sorted")]
        public async Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasks()
        {
            return await _service.GetSortedUsersWithSortedTasksAsync();
        }

        [HttpGet("/info/{userId}")]
        public async Task<UserInfoDto> GetUserInfo(int userId)
        {
            return await _service.GetUserInfoAsync(userId);
        }
    }
}
