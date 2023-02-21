using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.BL.MappingProfiles;
using CollectionsAndLinq.BL.Models;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IDataProcessingService _service;

        public ProjectsController(IDataProcessingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ProjectDto>> GetProjects()
        {
            return _service.GetProjectsAsync().Result;
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectDto> GetProject(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult<ProjectDto> AddProject([FromBody] NewProjectDto project)
        {
            return View();
        }

        [HttpPut]
        public ActionResult<Project> EditProject([FromBody] NewProjectDto project)
        {
            return View();
        }

        [HttpDelete("{id}")]
        public ActionResult<ProjectDto> DeleteProject(int id)
        {
            return View();
        }

        [HttpGet("byTeamSize/{teamSize}")]
        public ActionResult<List<(int Id, string Name)>> GetProjectsByTeamSize(int teamSize)
        {
            return _service.GetProjectsByTeamSizeAsync(teamSize).Result;
        }

        [HttpGet("info")]
        public ActionResult<List<ProjectInfoDto>> GetProjectsInfoAsync()
        {
            return _service.GetProjectsInfoAsync().Result;
        }

        [HttpGet("sortedFiltered")]
        public ActionResult<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjects
            (PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
        {
        }
    }
}
