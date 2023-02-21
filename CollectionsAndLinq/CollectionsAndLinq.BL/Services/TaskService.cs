using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Task = CollectionsAndLinq.DAL.Entities.Task.Task;

namespace CollectionsAndLinq.BL.Services
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<TaskDto>> GetTasksAsync()
        {
            return await _context.Tasks.Select(x => _mapper.Map<TaskDto>(x)).ToListAsync();
        }

        public async Task<TaskDto> GetTaskAsync(int id)
        {
            return await _context.Tasks.Where(x => x.Id == id).Select(x => _mapper.Map<TaskDto>(x)).FirstAsync();
        }

        public async Task<TaskDto> CreateTask(NewTaskDto task)
        {
            var taskEntity = _mapper.Map<Task>(task);

            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            var createdTask = await _context.Tasks
                .FirstAsync(x => x.Id == task.Id);

            return _mapper.Map<TaskDto>(createdTask);
        }

        public async Task<TaskDto> UpdateTask(NewTaskDto task)
        {
            var taskEntity = _mapper.Map<Task>(task);
            var removeEntity = await _context.Tasks.FirstAsync(x => x.Id == task.Id);

            _context.Tasks.Remove(removeEntity);
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.Tasks.FirstAsync(x => x.Id == task.Id);

            return _mapper.Map<TaskDto>(updatedEntity);
        }

        public async Task<TaskDto> DeleteTask(int id)
        {
            var taskEntity = await _context.Tasks.FirstAsync(x => x.Id == id);

            _context.Tasks.Remove(taskEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDto>(taskEntity);
        }

        public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
        {
            var projects = await _context.Projects.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();

            var tasksByUser = projects
                .Where(project => project.AuthorId == userId)
                .GroupJoin(tasks,
                project => project.Id,
                task => task.ProjectId,
                (project, tasksByUser) => (
                    IdName: $"{project.Id}:{project.Name}",
                    numOfTasks: tasksByUser.Where(task => task.ProjectId == project.Id).Count()))
                .ToDictionary(key => key.IdName, value => value.numOfTasks);

            return tasksByUser;
        }

        public async Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId)
        {
            var tasks = await _context.Tasks.ToListAsync();

            var capitalTasksByUser = tasks
                .Where(task => task.PerformerId == userId)
                .OrderBy(task => task.Name)
                .ToList();

            return _mapper.Map<List<TaskDto>>(capitalTasksByUser);
        }
    }
}
