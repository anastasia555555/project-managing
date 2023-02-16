using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Models.Projects;
using CollectionsAndLinq.BL.Models.Tasks;
using CollectionsAndLinq.BL.Models.Users;

namespace CollectionsAndLinq.BL.Entities
{
    public record UserInfo(
    User User,
    Project LastProject,
    int LastProjectTasksCount,
    int NotFinishedOrCanceledTasksCount,
    Task LongestTask)
    {

    }
}
