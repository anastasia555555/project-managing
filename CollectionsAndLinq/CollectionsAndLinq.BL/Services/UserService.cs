using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Models.Users;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ThreadContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<List<UserDto>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> CreateUser(NewUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateUser(NewUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserWithTasksDto>> GetSortedUsersWithSortedTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoDto> GetUserInfoAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
