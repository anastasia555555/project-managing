namespace CollectionsAndLinq.DAL.Entities.Project
{
    public record ProjectInfo(
    Project Project,
    Task.Task LongestTaskByDescription,
    Task.Task ShortestTaskByName,
    int? TeamMembersCount = null)
    {

    }
}
