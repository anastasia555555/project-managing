using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Users;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.BL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await _context.Users.Select(x => _mapper.Map<UserDto>(x)).ToListAsync();
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            return await _context.Users.Where(x => x.Id == id).Select(x => _mapper.Map<UserDto>(x)).FirstAsync();
        }

        public async Task<UserDto> CreateUser(NewUserDto user)
        {
            var userEntity = _mapper.Map<User>(user);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            var createdUser = await _context.Users
                .FirstAsync(x => x.Id == user.Id);

            return _mapper.Map<UserDto>(createdUser);
        }

        public async Task<UserDto> UpdateUser(NewUserDto user)
        {
            var userEntity = _mapper.Map<User>(user);
            var removeEntity = await _context.Users.FirstAsync(x => x.Id == user.Id);

            _context.Users.Remove(removeEntity);
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.Users.FirstAsync(x => x.Id == user.Id);

            return _mapper.Map<UserDto>(updatedEntity);
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            var userEntity = await _context.Users.FirstAsync(x => x.Id == id);

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(userEntity);
        }

        public async Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
        {
            var users = await _context.Users.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();

            var usersWithTasks = users
                .GroupJoin(tasks,
                user => user.Id,
                task => task.PerformerId,
                (user, tasksOfUser) => new UserWithTasks(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.RegisteredAt,
                    user.BirthDay,
                    tasksOfUser.OrderByDescending(x => x.Name.Length).ToList()))
                .OrderBy(user => user.FirstName);

            return _mapper.Map<List<UserWithTasksDto>>(usersWithTasks);
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int userId)
        {
            var projects = await _context.Projects.ToListAsync();
            var users = await _context.Users.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();

            if (!users.Any(user => user.Id == userId))
            {
                return null;
            }

            var userInfo = users
                .Where(user => user.Id == userId)
                .GroupJoin(projects,
                user => user.Id,
                project => project.AuthorId,
                (user, project) =>
                    (user, lastProject: project.MaxBy(p => p.CreatedAt)))
                .GroupJoin(tasks,
                userWithProject => userWithProject.lastProject is null ? -1 : userWithProject.lastProject.Id,
                task => task.ProjectId,
                (userWithProject, tasks) => new UserInfo(
                    userWithProject.user,
                    userWithProject.lastProject,
                    tasks.Count(),
                    tasks.Where(t => t.State != TaskState.Done).Count(),
                    tasks.MaxBy(t => (t.State == TaskState.InProgress ? DateTime.Now : (t.FinishedAt ?? DateTime.MinValue)) - t.CreatedAt)))
                .First();


            return _mapper.Map<UserInfoDto>(userInfo);
        }
    }
}
