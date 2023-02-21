using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetTasksAsync();
        Task<TaskDto> GetTaskAsync(int id); 
        Task<TaskDto> CreateTask(NewTaskDto task);
        Task<TaskDto> UpdateTask(NewTaskDto task);
        Task<TaskDto> DeleteTask(int id);
        Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId);
        Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId);
    }
}
