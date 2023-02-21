using AutoMapper;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.DAL.Entities.Task;
using Task = CollectionsAndLinq.DAL.Entities.Task.Task;

namespace CollectionsAndLinq.BL.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDto>()
                .ForMember(dest => dest.Name, scr => scr.MapFrom(task => MappingHelper.Capitalize(task.Name)))
                .ForMember(dest => dest.State, scr => scr.MapFrom(task => MappingHelper.TaskStateToString(task.State)));
            CreateMap<TaskWithPerfomer, TaskWithPerfomerDto>()
                .ForMember(dest => dest.Name, scr => scr.MapFrom(task => MappingHelper.Capitalize(task.Name)))
                .ForMember(dest => dest.State, scr => scr.MapFrom(task => MappingHelper.TaskStateToString(task.State)));
            CreateMap<NewTaskDto, Task>()
                .ForMember(dest => dest.Name, scr => scr.MapFrom(task => MappingHelper.Capitalize(task.Name)))
                .ForMember(dest => dest.State, scr => scr.MapFrom(task => MappingHelper.TaskStateToString(task.State)));
        }
    }
}
