using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Teams;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamsController(ITeamService service) => _service = service;

        [HttpGet]
        public async Task<List<TeamDto>> GetTeams()
        {
            return await _service.GetTeamsAsync();
        }

        [HttpGet("{id}")]
        public async Task<TeamDto> GetTeam(int id)
        {
            return await _service.GetTeamAsync(id);
        }

        [HttpPost]
        public async Task<TeamDto> AddTeam([FromBody] NewTeamDto team)
        {
            return await _service.CreateTeam(team);
        }

        [HttpPut]
        public async Task<TeamDto> EditTeam([FromBody] NewTeamDto team)
        {
            return await _service.UpdateTeam(team);
        }

        [HttpDelete("{id}")]
        public async Task<TeamDto> DeleteTeam(int id)
        {
            return await _service.DeleteTeam(id);
        }

        [HttpGet("/sortedByYaer/{year}")]
        public async Task<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYear(int year)
        {
            return await _service.GetSortedTeamByMembersWithYearAsync(year);
        }
    }
}
