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

            CreateMap<UserWithTasks, UserWithTasksDto>();

            CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
