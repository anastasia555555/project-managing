namespace CollectionsAndLinq.DAL.Entities.User
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
