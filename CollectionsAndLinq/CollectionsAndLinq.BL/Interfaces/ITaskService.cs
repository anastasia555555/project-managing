using CollectionsAndLinq.BL.Models.Tasks;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetTasksAsync();
        Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId);
        Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId);
    }
}
