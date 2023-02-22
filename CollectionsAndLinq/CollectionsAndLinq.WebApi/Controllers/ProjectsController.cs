using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service) => _service = service;

        [HttpGet]
        public async Task<List<ProjectDto>> GetProjects()
        {
            return await _service.GetProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProjectDto> GetProject(int id)
        {
            return await _service.GetProjectAsync(id);
        }

        [HttpPost]
        public async Task<ProjectDto> AddProject([FromBody] NewProjectDto project)
        {
            return await _service.CreateProject(project);
        }

        [HttpPut]
        public async Task<ProjectDto> EditProject([FromBody] NewProjectDto project)
        {
            return await _service.UpdateProject(project);
        }

        [HttpDelete("{id}")]
        public async Task<ProjectDto> DeleteProject(int id)
        {
            return await _service.DeleteProject(id);
        }

        [HttpGet("byTeamSize/{teamSize}")]
        public async Task<List<(int Id, string Name)>> GetProjectsByTeamSize(int teamSize)
        {
            return await _service.GetProjectsByTeamSizeAsync(teamSize);
        }

        [HttpGet("info")]
        public async Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
        {
            return await _service.GetProjectsInfoAsync();
        }

        [HttpGet("SortedFiltered")]
        public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjects
            ([FromBody] PageParams pageParams)
        {
            return await _service.GetSortedFilteredPageOfProjectsAsync
                (pageParams.pageModel, pageParams.filterModel, pageParams.sortingModel);
        }
    }
}
