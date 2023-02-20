using CollectionsAndLinq.BL.Models.Teams;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamDto>> GetTeamsAsync();
        Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYearAsync(int year);
    }
}
