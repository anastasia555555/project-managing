using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities.Project;
using CollectionsAndLinq.DAL.Entities.Task;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.BL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(CollectionsAndLinqContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            return await _context.Projects.Select(x => _mapper.Map<ProjectDto>(x)).ToListAsync();
        }

        public async Task<ProjectDto> GetProjectAsync(int id)
        {
            return await _context.Projects.Where(x => x.Id == id).Select(x => _mapper.Map<ProjectDto>(x)).FirstAsync();
        }

        public async Task<ProjectDto> CreateProject(NewProjectDto project)
        {
            var projectEntity = _mapper.Map<Project>(project);

            _context.Projects.Add(projectEntity);
            await _context.SaveChangesAsync();

            var createdProject = await _context.Projects
                .FirstAsync(x => x.Id == project.Id);

            return _mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<ProjectDto> UpdateProject(NewProjectDto project)
        {
            var projectEntity = _mapper.Map<Project>(project);
            var removeEntity = await _context.Projects.FirstAsync(x => x.Id == project.Id);

            _context.Projects.Remove(removeEntity);
            _context.Projects.Add(projectEntity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.Projects.FirstAsync(x => x.Id == project.Id);

            return _mapper.Map<ProjectDto>(updatedEntity);
        }

        public async Task<ProjectDto> DeleteProject(int id)
        {
            var projectEntity = await _context.Projects.FirstAsync(x => x.Id == id);

            _context.Projects.Remove(projectEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(projectEntity);
        }

        public async Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize)
        {
            var projects = await _context.Projects.ToListAsync();
            var teams = await _context.Teams.ToListAsync();
            var users = await _context.Users.ToListAsync();


            var projectsByTeamSize = teams
                .GroupJoin(users,
                team => team.Id,
                user => user.TeamId,
                (team, usersInTeam) => (
                    team.Id,
                    usersInTeam: usersInTeam.Count(u => u.TeamId == team.Id)))
                .Where(t => t.usersInTeam > teamSize)
                .Join(projects,
                team => team.Id,
                project => project.TeamId,
                (team, project) =>
                    (project.Id, project.Name))
                .ToList();

            return projectsByTeamSize ?? new List<(int Id, string Name)>();
        }

        public async Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
        {
            var projects = await _context.Projects.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();
            var users = await _context.Users.ToListAsync();

            var projectsInfo = projects
                .GroupJoin(tasks,
                project => project.Id,
                task => task.ProjectId,
                (project, tasksInProject) => (
                    project,
                    maxTaskByDescribtion: tasksInProject.MaxBy(t => t.Description),
                    maxTaskByNameLength: tasksInProject.MaxBy(t => t.Name.Length),
                    tasksInProject))
                .GroupJoin(users,
                 projWithTasks => projWithTasks.project.TeamId,
                 user => user.TeamId,
                 (projWithTasks, usersInTeam) => new ProjectInfo(
                    projWithTasks.project,
                    projWithTasks.maxTaskByDescribtion,
                    projWithTasks.maxTaskByNameLength,
                    projWithTasks.project.Description.Length > 20 || projWithTasks.tasksInProject.Count() < 3 ? 
                    usersInTeam.Count() : null))
                .ToList();

            return _mapper.Map<List<ProjectInfoDto>>(projectsInfo);
        }

        public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync
            (PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
        {
            var projects = await _context.Projects.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();
            var users = await _context.Users.ToListAsync();
            var teams = await _context.Teams.ToListAsync();

            Func<FullProject, bool> filtering = (x) => true;

            if (filterModel != null)
            {
                 filtering = (x) =>
                x.Name.Contains(filterModel.Name ?? string.Empty) &&
                x.Description.Contains(filterModel.Description ?? string.Empty) &&
                x.Author.FirstName.Contains(filterModel.AutorFirstName ?? string.Empty) &&
                x.Author.LastName.Contains(filterModel.AutorLastName ?? string.Empty) &&
                x.Team.Name.Contains(filterModel.TeamName ?? string.Empty);
            }
            
            Func<FullProject, object> sortingAscending = (x) => false;
            Func<FullProject, object> sortingDescending = (x) => false;

            if (sortingModel != null)
            {
                Func<FullProject, object> sorting = (x) => true;

                switch (sortingModel.Property)
                {
                    case SortingProperty.Name:
                        sorting = (x) => x.Name;
                        break;
                    case SortingProperty.Description:
                        sorting = (x) => x.Description;
                        break;
                    case SortingProperty.Deadline:
                        sorting = (x) => x.Deadline;
                        break;
                    case SortingProperty.CteatedAt:
                        sorting = (x) => x.CreatedAt;
                        break;
                    case SortingProperty.TasksCount:
                        sorting = (x) => x.Tasks.Count;
                        break;
                    case SortingProperty.AutorFirstName:
                        sorting = (x) => x.Author.FirstName;
                        break;
                    case SortingProperty.AutorLastName:
                        sorting = (x) => x.Author.LastName;
                        break;
                    case SortingProperty.TeamName:
                        sorting = (x) => x.Team.Name;
                        break;
                    default:
                        break;
                }

                switch (sortingModel.Order)
                {
                    case SortingOrder.Ascending:
                        sortingAscending = sorting;
                        break;
                    case SortingOrder.Descending:
                        sortingDescending = sorting;
                        break;
                    default:
                        break;
                }
                
            }

            int skipPages = 0;

            if(pageModel != null)
            {
                skipPages = pageModel.PageSize * (pageModel.PageNumber - 1);
            }

            var sortedFilteredPagedProjects = projects
                .GroupJoin(tasks
                .Join(users,
                task => task.PerformerId,
                user => user.Id,
                (task, user) => new TaskWithPerfomer(
                    task.Id,
                    task.Name,
                    task.Description,
                    task.ProjectId,
                    task.State,
                    task.CreatedAt,
                    task.FinishedAt,
                    user)),
                project => project.Id,
                task => task.ProjectId,
                (project, tasksInProject) =>
                    (project, tasksInProject))
                .Join(users,
                projWithTasks => projWithTasks.project.AuthorId,
                user => user.Id,
                (projWithTasks, user) =>
                    (projWithTasks, user))
                .Join(teams,
                proj => proj.projWithTasks.project.TeamId,
                team => team.Id,
                (proj, team) => new FullProject(
                    proj.projWithTasks.project.Id,
                    proj.projWithTasks.project.Name,
                    proj.projWithTasks.project.Description,
                    proj.projWithTasks.project.CreatedAt,
                    proj.projWithTasks.project.Deadline,
                    proj.projWithTasks.tasksInProject.ToList(),
                    proj.user,
                    team))
                .Where(filtering)
                .OrderBy(sortingAscending)
                .OrderByDescending(sortingDescending)
                .Skip(skipPages)
                .Take(pageModel.PageSize)
                .ToList();

            return new PagedList<FullProjectDto>(_mapper.Map<List<FullProjectDto>>(sortedFilteredPagedProjects), sortedFilteredPagedProjects.Count);
        }
    }
}
