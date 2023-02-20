using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(ThreadContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public Task<List<TaskDto>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> CreateTask(NewTaskDto task)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> UpdateTask(NewTaskDto task)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> DeleteTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, int>> GetTasksCountInProjectsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskDto>> GetCapitalTasksByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
