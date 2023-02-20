using CollectionsAndLinq.DAL.Entities;
using Task = CollectionsAndLinq.DAl.Entities.Task;

namespace CollectionsAndLinq.BL.Interfaces;

public interface IDataProvider
{
    Task<List<Project>> GetProjectsAsync();
    Task<List<Task>> GetTasksAsync();
    Task<List<Team>> GetTeamsAsync();
    Task<List<User>> GetUsersAsync();
}
