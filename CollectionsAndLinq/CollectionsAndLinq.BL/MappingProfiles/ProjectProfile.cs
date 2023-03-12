using AutoMapper;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.DAL.Entities;
using CollectionsAndLinq.DAL.Entities.Project;

namespace CollectionsAndLinq.BL.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectInfo, ProjectInfoDto>();
            CreateMap<FullProject, FullProjectDto>();
            CreateMap<NewProjectDto, Project>();
        }
    }
}
