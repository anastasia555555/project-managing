using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service) => _service = service;

        [HttpGet]
        public async Task<List<TaskDto>> GetTasks()
        {
            return await _service.GetTasksAsync();
        }

        [HttpGet("{id}")]
        public async Task<TaskDto> GetTaskAsync(int id)
        {
            return await _service.GetTaskAsync(id);
        }

        [HttpPost]
        public async Task<TaskDto> AddTask([FromBody] NewTaskDto task)
        {
            return await _service.CreateTask(task);
        }

        [HttpPut]
        public async Task<TaskDto> EditTask([FromBody] NewTaskDto task)
        {
            return await _service.UpdateTask(task);
        }

        [HttpDelete("{id}")]
        public async Task<TaskDto> DeleteTask(int id)
        {
            return await _service.DeleteTask(id);
        }

        [HttpGet("/countInProjectsByUser/{userId}")]
        public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserId(int userId)
        {
            return await _service.GetTasksCountInProjectsByUserIdAsync(userId);
        }

        [HttpGet("/byUser/{userId}")]
        public async Task<List<TaskDto>> GetCapitalTasksByUserId(int userId)
        {
            return await _service.GetCapitalTasksByUserIdAsync(userId);
        }
    }
}
