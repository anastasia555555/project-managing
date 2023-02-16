using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Entities
{
    public record ProjectInfo(
    Project Project,
    Task LongestTaskByDescription,
    Task ShortestTaskByName,
    int? TeamMembersCount = null)
    {

    }
}
