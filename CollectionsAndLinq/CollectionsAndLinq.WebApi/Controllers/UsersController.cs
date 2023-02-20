using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IDataProcessingService _service;

        public UsersController(IDataProcessingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetUsers()
        {
            return _service.GetUsersAsync().Result;
        }

        [HttpGet("{id}")]
        public ActionResult<TeamDto> GetUser(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User team)
        {
            return View();
        }

        [HttpPut]
        public ActionResult<User> EditUser([FromBody] User team)
        {
            return View();
        }

        [HttpDelete("{id}")]
        public ActionResult<UserDto> DeleteUser(int id)
        {
            return View();
        }

        [HttpGet("/sorted")]
        public ActionResult<List<UserWithTasksDto>> GetSortedUsersWithSortedTasks()
        {
            return _service.GetSortedUsersWithSortedTasksAsync().Result;
        }

        [HttpGet("/info/{userId}")]
        public ActionResult<UserInfoDto> GetUserInfo(int userId)
        {
            return _service.GetUserInfoAsync(userId).Result;
        }
    }
}
