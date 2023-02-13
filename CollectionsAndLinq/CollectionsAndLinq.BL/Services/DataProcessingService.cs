using AutoMapper.Features;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using AutoMapper;
using CollectionsAndLinq.BL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CollectionsAndLinq.BL.Services;

public class DataProcessingService : IDataProcessingService
{
    private readonly IDataProvider _dataProvider;
    private readonly IMapper _mapper;


    public DataProcessingService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public DataProcessingService(IDataProvider dataProvider, IMapper mapper)
    {
        _dataProvider = dataProvider;
        _mapper = mapper;
    }

    public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
    {
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var result = projects.Where(project => project.AuthorId == userId)
                             .GroupJoin(tasks,
                                project => project.Id,
                                task => task.ProjectId,
                                (project, tasksByUser) =>
                                new KeyValuePair<string, int>
                                (
                                    $"{project.Id}:{project.Name}", 
                                    tasksByUser.Where(task => task.ProjectId == project.Id).Count()
                                ));

        Dictionary<string, int> dict = new();

        foreach(var res in result)
        {
            dict.Add(res.Key, res.Value);
        }
        return dict;

    }

    public async Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId)
    {
        var tasks = await _dataProvider.GetTasksAsync();

        var result = tasks.Where(task => task.PerformerId == userId).ToList();

        List<TaskDto> tasksByUser = new();

        if(result != null)
        {
            tasksByUser =_mapper.Map<List<TaskDto>>(result);
        }

        return tasksByUser;
    }

    public async Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize)
    {
        var projects = await _dataProvider.GetProjectsAsync();
        var teams = await _dataProvider.GetTeamsAsync();
        var users = await _dataProvider.GetUsersAsync();

        var result = teams.GroupJoin(users,
                                team => team.Id,
                                user => user.TeamId,
                                (team, usersInTeam) =>
                                (
                                    team.Id, 
                                    usersInTeam.Where(u => u.TeamId == team.Id).Count()
                                ))
                                .Where((t, u) => u > teamSize)
                                .Join(projects,
                                team => team.Id,
                                project => project.TeamId,
                                (team, project) => 
                                (project.Id, project.Name));

        List<(int Id, string Name)> proj = new();

        if (result != null)
        {
            proj = _mapper.Map<List<(int Id, string Name)>>(result);
        }

        return proj;

    }


    public async Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
    {
        var teams = await _dataProvider.GetTeamsAsync();
        var users = await _dataProvider.GetUsersAsync();

        var halfresults = teams.GroupJoin(users,
                               team => team.Id,
                               user => user.TeamId,
                               (team, usersInTeam) =>
                               (team, usersInTeam.OrderByDescending(x => x.RegisteredAt)))
                               .Where(p => p.Item2.All(u => u.BirthDay.Year < year))
                               .OrderBy(p => p.team.Name);
                               //.ThenByDescending(u => u.usersInTeam.OrderByDescending(x => x.RegisteredAt)); //this I don't like

        List<TeamWithMembersDto> result = new();

        foreach (var halfresult in halfresults)
        {
            var team = _mapper.Map<TeamWithMembersDto>(halfresult); //trouble with mapping
            team.Members.AddRange(_mapper.Map<IEnumerable<UserDto>>(halfresult.Item2));

            if(team.Members.Count == 0)
            {
                continue;
            }

            result.Add(team);
        }

        return result;
    }

    public async Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
    {
        var users = await _dataProvider.GetUsersAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var halfresults = users.GroupJoin(tasks,
                              user => user.Id,
                              task => task.PerformerId,
                              (user, tasksOfUser) =>
                              (user, tasksOfUser.OrderByDescending(x => x.Name.Length)))
                              .OrderBy(p => p.user.FirstName);

        List<UserWithTasksDto> result = new();

        foreach (var halfresult in halfresults)
        {
            var user = _mapper.Map<UserWithTasksDto>(halfresult); //trouble with mapping
            user.Tasks.AddRange(_mapper.Map<IEnumerable<TaskDto>>(halfresult.Item2));

            result.Add(user);
        }

        return result;
    }

    public async Task<UserInfoDto> GetUserInfoAsync(int userId)
    {

        var users = await _dataProvider.GetUsersAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        var halfresults = users.Where(user => user.Id == userId) //trouble with query
                          .GroupJoin(projects,
                              user => user.Id,
                              project => project.AuthorId,
                              (User, project) =>
                              (User, project.OrderByDescending(p => p.CreatedAt).First()))
                           .GroupJoin(tasks,
                              pair => pair.Item2.Id,
                              task => task.ProjectId,
                              (pair, tasks) =>
                              (
                                    pair, 
                                    tasks.Count(), 
                                    tasks.Where(t => t.State != TaskState.Done), 
                                    tasks.OrderByDescending(t => (t.FinishedAt ?? DateTime.Now) - t.CreatedAt).First()
                               ));

        UserInfoDto user;
        foreach (var halfresult in halfresults)
        {
            if (halfresult.pair.User == null)
            {
                return null;
            }

            user = _mapper.Map<UserInfoDto>(halfresult);
            return user;
        }

        return null;
    }

    public Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
    {
                           // .GroupJoin(tasks,
                           //   pair => pair.Item2.Id,
                           //   task => task.ProjectId,
                           //   (pair, tasksInProject) =>
                           //   (pair, tasksInProject.OrderByDescending(t => t.Description.Length).First()))
                           //.GroupJoin(tasks,
                           //   trinity => trinity.pair.Item2.Id,
                           //   task => task.ProjectId,
                           //   (trinity, tasksInProject) =>
                           //   (trinity, tasksInProject.OrderBy(t => t.Name.Length).First()))
                           //.GroupJoin(tasks,
                           //   four => four.trinity.pair.user.Id,
                           //   task => task.PerformerId,
                           //   (trinity, tasksByUser) =>
                           //   (trinity, tasksByUser.OrderByDescending(t => (t.FinishedAt ?? DateTime.Now) - t.CreatedAt).First()));
        throw new NotImplementedException();
    }

    public Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
    {
        throw new NotImplementedException();
    }
}
