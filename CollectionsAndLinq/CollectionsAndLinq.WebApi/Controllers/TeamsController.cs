using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Teams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : Controller
    {
        private readonly IDataProcessingService _service;

        public TeamsController(IDataProcessingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<TeamDto>> GetTeams()
        {
            return _service.GetTeamsAsync().Result;
        }

        [HttpGet("{id}")]
        public ActionResult<TeamDto> GetTeam(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult<Team> AddTeam([FromBody] Team team)
        {
            return View();
        }

        [HttpPut]
        public ActionResult<Team> EditTeam([FromBody] Team team)
        {
            return View();
        }

        [HttpDelete("{id}")]
        public ActionResult<TeamDto> DeleteTeam(int id)
        {
            return View();
        }

        [HttpGet("/sortedByYaer/{year}")]
        public ActionResult<List<TeamWithMembersDto>> GetSortedTeamByMembersWithYear(int year)
        {
            return _service.GetSortedTeamByMembersWithYearAsync(year).Result;
        }
    }
}
