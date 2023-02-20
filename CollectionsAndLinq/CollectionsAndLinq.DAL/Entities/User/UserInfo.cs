namespace CollectionsAndLinq.DAL.Entities.User
{
    public record UserInfo(
    User User,
    Project.Project LastProject,
    int LastProjectTasksCount,
    int NotFinishedOrCanceledTasksCount,
    Task.Task LongestTask)
    {

    }
}
