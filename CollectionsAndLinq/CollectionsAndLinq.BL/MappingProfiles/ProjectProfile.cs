using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Entities;

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
