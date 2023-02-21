using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Teams;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetTeamsAsync();
        Task<TeamDto> GetTeamAsync(int id);
        Task<TeamDto> CreateTeam(NewTeamDto team);
        Task<TeamDto> UpdateTeam(NewTeamDto team);
        Task<TeamDto> DeleteTeam(int id);
        Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year);
    }
}
