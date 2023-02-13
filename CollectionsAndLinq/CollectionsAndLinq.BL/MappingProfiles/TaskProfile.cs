using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsAndLinq.BL.Models.Tasks;

namespace CollectionsAndLinq.BL.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Entities.Task, TaskDto>()
                .ForMember(dest => dest.Name, scr => scr.MapFrom(task => MappingHelper.Capitalize(task.Name)))
                .ForMember(dest => dest.State, scr => scr.MapFrom(task => MappingHelper.TaskStateToString(task.State)));
        }
    }
}
