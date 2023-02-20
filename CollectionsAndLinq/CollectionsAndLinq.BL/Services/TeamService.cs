using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class TeamService : BaseService, ITeamService
    {
        public TeamService(ThreadContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Task<List<TeamDto>> GetTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto> CreateTeam(NewTeamDto team)
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto> UpdateTeam(NewTeamDto team)
        {
            throw new NotImplementedException();
        }

        public Task<TeamDto> DeleteTeam(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year)
        {
            throw new NotImplementedException();
        }
    }
}
