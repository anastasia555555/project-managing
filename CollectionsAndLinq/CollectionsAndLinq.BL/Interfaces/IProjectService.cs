using CollectionsAndLinq.BL.Models;
using CollectionsAndLinq.BL.Models.Projects;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetProjectsAsync();
        Task<List<(int Id, string Name)>> GetProjectsByTeamSizeAsync(int teamSize);
        Task<List<ProjectInfoDto>> GetProjectsInfoAsync();
        Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel);
    }
}
