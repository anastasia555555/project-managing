 using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities.Project;
using CollectionsAndLinq.DAL.Entities.Task;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            return await _context.Projects.Select(x => _mapper.Map<ProjectDto>(x)).ToListAsync();
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

            var result = projects
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

            return _mapper.Map<List<ProjectInfoDto>>(result);
        }

        public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync
            (PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
        {
            var projects = await _context.Projects.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();
            var users = await _context.Users.ToListAsync();
            var teams = await _context.Teams.ToListAsync();

            Func<FullProject, bool> filtering = (_) => true;

            if (filterModel != null)
            {
                 filtering = (_) =>
                _.Name.Contains(filterModel.Name ?? string.Empty) &&
                _.Description.Contains(filterModel.Description ?? string.Empty) &&
                _.Author.FirstName.Contains(filterModel.AutorFirstName ?? string.Empty) &&
                _.Author.LastName.Contains(filterModel.AutorLastName ?? string.Empty) &&
                _.Team.Name.Contains(filterModel.TeamName ?? string.Empty);
            }
            
            Func<FullProject, bool> sorting = (_) => true;

            if(sortingModel != null)
            {
                sorting = (_) => false; //finish tomorrow
            }

            var result = projects
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
                .ToList(); 

            /*var task_user = tasks
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
                    user));

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
                if (filterModel.Name is not null)
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
            }*/

            if (sortingModel is not null)
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

            if (pageModel is not null)
            {
                List<FullProjectDto> proj = new();

                int firstItem = pageModel.PageSize * (pageModel.PageNumber - 1);
                int lastItem = pageModel.PageSize * pageModel.PageNumber;

                for (int i = firstItem; i <= lastItem; i++)
                {
                    if (Projects.Count <= i)
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
}
