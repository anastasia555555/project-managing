using AutoMapper;
using CollectionsAndLinq.BL.Models.Users;
using CollectionsAndLinq.DAL.Entities.User;


namespace CollectionsAndLinq.BL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserWithTasks, UserWithTasksDto>();

            CreateMap<UserInfo, UserInfoDto>();

            CreateMap<NewUserDto, User>();
        }
    }
}
