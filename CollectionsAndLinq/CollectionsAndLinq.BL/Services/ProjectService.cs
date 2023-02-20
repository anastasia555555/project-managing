using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities.Project;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(ThreadContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            var projects = await _context.Projects.ToListAsync();

            throw new NotImplementedException();
        }

        public Task<ProjectDto> CreateProject(NewProjectDto project)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto> UpdateProject(NewProjectDto project)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto> DeleteProject(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProjectInfoDto>> GetProjectsInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
        {
            throw new NotImplementedException();
        }
    }
}
