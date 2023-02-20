using AutoMapper.Features;
using System.Text.RegularExpressions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;
using CollectionsAndLinq.BL.MappingProfiles;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using AutoMapper;
using CollectionsAndLinq.BL.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;
using AutoMapper.Execution;

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

    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
        return _mapper.Map<List<ProjectDto>>(await _dataProvider.GetProjectsAsync());
    }

    public async Task<List<TaskDto>> GetTasksAsync()
    {
        return _mapper.Map<List<TaskDto>>(await _dataProvider.GetTasksAsync());
    }

    public async Task<List<TeamDto>> GetTeamsAsync()
    {
        return _mapper.Map<List<TeamDto>>(await _dataProvider.GetTeamsAsync());
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return _mapper.Map<List<UserDto>>(await _dataProvider.GetUsersAsync());
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

        var result = tasks.Where(task => task.PerformerId == userId).OrderBy(task => task.Name).ToList();

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
                                    usersInTeam.Count(u => u.TeamId == team.Id)
                                ))
                                .Where((t, u) => t.Item2 > teamSize)
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
                               new TeamWithMembers
                               (
                                   team.Id,
                                   team.Name,
                                   usersInTeam.OrderByDescending(x => x.RegisteredAt).ToList()
                               ))
                               .Where(team => team.Members.All(u => u.BirthDay.Year < year))
                               .OrderBy(team => team.Name);

        List<TeamWithMembersDto> result = new();

        foreach (var halfresult in halfresults)
        {
            var team = _mapper.Map<TeamWithMembersDto>(halfresult); 
           
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
                              new UserWithTasks
                              (
                                  user.Id,
                                  user.FirstName,
                                  user.LastName,
                                  user.Email,
                                  user.RegisteredAt,
                                  user.BirthDay,
                                  tasksOfUser.OrderByDescending(x => x.Name.Length).ToList()
                              ))
                              .OrderBy(user => user.FirstName);

        List<UserWithTasksDto> result = new();

        foreach (var halfresult in halfresults)
        {
            var user = _mapper.Map<UserWithTasksDto>(halfresult); 

            result.Add(user);
        }

        return result;
    }

    public async Task<UserInfoDto> GetUserInfoAsync(int userId)
    {

        var users = await _dataProvider.GetUsersAsync();
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();

        if(!users.Any(user => user.Id == userId))
        {
            return null;
        }

        var halfresult = users.Where(user => user.Id == userId) 
                          .GroupJoin(projects,
                              user => user.Id,
                              project => project.AuthorId,
                              (User, project) =>
                              (User, project.MaxBy(p => p.CreatedAt)))
                           .GroupJoin(tasks,
                              user_proj => user_proj.Item2 is null ? -1 : user_proj.Item2.Id,
                              task => task.ProjectId,
                              (user_proj, tasks) =>
                              new UserInfo
                              (
                                    user_proj.User,
                                    user_proj.Item2,
                                    tasks.Count(),
                                    tasks.Where(t => t.State != TaskState.Done).Count(), 
                                    tasks.MaxBy(t => (t.State == TaskState.InProgress ? DateTime.Now : (t.FinishedAt ?? DateTime.MinValue)) - t.CreatedAt)
                               )).Single();


        return _mapper.Map<UserInfoDto>(halfresult);
        
    }

    public async Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
    {
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();
        var users = await _dataProvider.GetUsersAsync();

        var halfresults = projects.GroupJoin(tasks,
                              project => project.Id,
                              task => task.ProjectId,
                              (project, tasksInProject) =>
                              (
                                project,
                                tasksInProject.MaxBy(t => t.Description),
                                tasksInProject.MaxBy(t => t.Name.Length), 
                                tasksInProject
                              ))
                                  .GroupJoin(users,
                              proj_tasks => proj_tasks.project.TeamId,
                              user => user.TeamId,
                              (proj_tasks, usersInTeam) =>
                              new ProjectInfo
                              (
                                    proj_tasks.project,
                                    proj_tasks.Item2,
                                    proj_tasks.Item3,
                                    proj_tasks.project.Description.Length > 20 || proj_tasks.tasksInProject.Count() < 3 ? usersInTeam.Count() : null
                               ));

        List<ProjectInfoDto> result = new();

        foreach (var halfresult in halfresults)
        {
            var projecInfo = _mapper.Map<ProjectInfoDto>(halfresult);
            result.Add(projecInfo);
        }

        return result;
    }

    public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
    {
        var projects = await _dataProvider.GetProjectsAsync();
        var tasks = await _dataProvider.GetTasksAsync();
        var users = await _dataProvider.GetUsersAsync();
        var teams = await _dataProvider.GetTeamsAsync();


        var task_user = tasks.Join(users,
                        task => task.PerformerId,
                        user => user.Id,
                        (task, user) => 
                        new TaskWithPerfomer
                        (
                            task.Id,
                            task.Name,
                            task.Description,
                            task.ProjectId,
                            task.State,
                            task.CreatedAt,
                            task.FinishedAt,
                            user
                         ));

        var proj_tasks = projects.GroupJoin(task_user,
                        project => project.Id,
                        task => task.ProjectId,
                        (project, tasksInProject) =>
                        (project, tasksInProject));

        var proj_user_team = proj_tasks.Join(users,
                                proj_tasks => proj_tasks.project.AuthorId,
                                user => user.Id,
                                (proj_tasks, user) =>
                                (proj_tasks, user))
                                .Join(teams,
                                proj_tasks_user => proj_tasks_user.proj_tasks.project.TeamId,
                                team => team.Id,
                                (proj_tasks_user, team) =>
                                new FullProject
                                (
                                    proj_tasks_user.proj_tasks.project.Id,
                                    proj_tasks_user.proj_tasks.project.Name,
                                    proj_tasks_user.proj_tasks.project.Description,
                                    proj_tasks_user.proj_tasks.project.CreatedAt,
                                    proj_tasks_user.proj_tasks.project.Deadline,
                                    proj_tasks_user.proj_tasks.tasksInProject.ToList(),
                                    proj_tasks_user.user,
                                    team
                                ));


        List<FullProjectDto> Projects = new();

        foreach (var p in proj_user_team)
        {
            var project = _mapper.Map<FullProjectDto>(p);
            Projects.Add(project);
        }

        if (filterModel is not null)
        {
            if(filterModel.Name is not null)
            {
                Projects = Projects.Where(p => p.Name.Contains(filterModel.Name)).ToList();
            }

            if (filterModel.Description is not null)
            {
                Projects = Projects.Where(p => p.Description.Contains(filterModel.Description)).ToList();
            }

            if (filterModel.AutorFirstName is not null)
            {
                Projects = Projects.Where(p => p.Author.FirstName.Contains(filterModel.AutorFirstName)).ToList();
            }

            if (filterModel.AutorLastName is not null)
            {
                Projects = Projects.Where(p => p.Author.LastName.Contains(filterModel.AutorLastName)).ToList();
            }

            if (filterModel.TeamName is not null)
            {
                Projects = Projects.Where(p => p.Team.Name.Contains(filterModel.TeamName)).ToList();
            }
        }

        if(sortingModel is not null)
        {
            switch (sortingModel.Property)
            {
                case SortingProperty.Name:
                    Projects = sortingModel.Order == SortingOrder.Ascending ? 
                        Projects.OrderBy(p => p.Name).ToList() : Projects.OrderByDescending(p => p.Name).ToList();
                    break;
                case SortingProperty.Description:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.Description).ToList() : Projects.OrderByDescending(p => p.Description).ToList();
                    break;
                case SortingProperty.Deadline:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.Deadline).ToList() : Projects.OrderByDescending(p => p.Deadline).ToList();
                    break;
                case SortingProperty.CteatedAt:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.CreatedAt).ToList() : Projects.OrderByDescending(p => p.CreatedAt).ToList();
                    break;
                case SortingProperty.TasksCount:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.Tasks.Count).ToList() : Projects.OrderByDescending(p => p.Tasks.Count).ToList();
                    break;
                case SortingProperty.AutorFirstName:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.Author.FirstName).ToList() : Projects.OrderByDescending(p => p.Author.FirstName).ToList();
                    break;
                case SortingProperty.AutorLastName:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.Author.LastName).ToList() : Projects.OrderByDescending(p => p.Author.LastName).ToList();
                    break;
                case SortingProperty.TeamName:
                    Projects = sortingModel.Order == SortingOrder.Ascending ?
                        Projects.OrderBy(p => p.Team.Name).ToList() : Projects.OrderByDescending(p => p.Team.Name).ToList();
                    break;
                default:
                    break;
            }
        }

        if(pageModel is not null)
        {
            List<FullProjectDto> proj = new();

            int firstItem = pageModel.PageSize * (pageModel.PageNumber - 1);
            int lastItem = pageModel.PageSize * pageModel.PageNumber;

            for (int i = firstItem; i <= lastItem; i++) 
            {
                if(Projects.Count <= i)
                {
                    break;
                }

                proj.Add(Projects[i]);
            }

            return new PagedList<FullProjectDto>(proj, Projects.Count);
        }

        return new PagedList<FullProjectDto>(Projects, Projects.Count); 
    }
}
