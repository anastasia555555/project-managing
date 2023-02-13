using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace CollectionsAndLinq.BL.Services
{
    public class DataProvider : IDataProvider
    {
        private readonly HttpClient _client;

        public DataProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _client.GetFromJsonAsync<List<Project>>("https://bsa-dotnet.azurewebsites.net/api/Projects");
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _client.GetFromJsonAsync<Project>($"https://bsa-dotnet.azurewebsites.net/api/Projects/{projectId}");
        }

        public async Task<List<Entities.Task>> GetTasksAsync()
        {
            return await _client.GetFromJsonAsync<List<Entities.Task>>("https://bsa-dotnet.azurewebsites.net/api/Tasks");
        }

        public async Task<Entities.Task> GetTasksByIdAsync(int taskId)
        {
            return await _client.GetFromJsonAsync<Entities.Task>($"https://bsa-dotnet.azurewebsites.net/api/Tasks/{taskId}");
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return await _client.GetFromJsonAsync<List<Team>>("https://bsa-dotnet.azurewebsites.net/api/Teams");
        }

        public async Task<Team> GetTeamsByIdAsync(int teamId)
        {
            return await _client.GetFromJsonAsync<Team>($"https://bsa-dotnet.azurewebsites.net/api/Teams/{teamId}");
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _client.GetFromJsonAsync<List<User>>("https://bsa-dotnet.azurewebsites.net/api/Users");
        }


        public async Task<User> GetUsersByIdAsync(int userId)
        {
            return await _client.GetFromJsonAsync<User>($"https://bsa-dotnet.azurewebsites.net/api/Users/{userId}");
        }
    }
}