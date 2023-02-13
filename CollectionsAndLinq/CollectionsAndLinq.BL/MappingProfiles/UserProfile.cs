using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsAndLinq.BL.Models.Users;
using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Projects;


namespace CollectionsAndLinq.BL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<User, UserWithTasksDto>()
                .ForMember(dest => dest.Tasks, scr => scr.MapFrom(x => new List<TaskDto>()));

            CreateMap<User, UserInfoDto>()
                .ForMember(dest => dest.LastProject, scr => scr.MapFrom(x => x))
                .ForMember(dest => dest.LastProjectTasksCount, scr => scr.MapFrom(x => x))
                .ForMember(dest => dest.NotFinishedOrCanceledTasksCount, scr => scr.MapFrom(x => x))
                .ForMember(dest => dest.LongestTask, scr => scr.MapFrom(x => x));
        }
    }
}
