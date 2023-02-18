using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Models.Projects;

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
            return View();
        }

        [HttpGet]
        public ActionResult<ProjectDto> GetProject(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult<ProjectDto> AddProject([FromBody] Project project)
        {
            return View();
        }

        [HttpPut]
        public ActionResult<Project> EditProject([FromBody] Project project)
        {
            return View();
        }

        [HttpDelete]
        public ActionResult<ProjectDto> DeleteProject(int id)
        {
            return View();
        }

    }
}
