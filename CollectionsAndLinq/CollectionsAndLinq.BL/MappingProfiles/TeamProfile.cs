using AutoMapper;
using CollectionsAndLinq.BL.Models.Teams;
using CollectionsAndLinq.DAL.Entities.Team;

namespace CollectionsAndLinq.BL.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDto>();
            CreateMap<TeamWithMembers, TeamWithMembersDto>();
            CreateMap<NewTeamDto, Team>();
        }
    }
}
