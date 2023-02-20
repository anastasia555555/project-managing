namespace CollectionsAndLinq.DAL.Entities.Project
{
    public record ProjectInfo(
    Project Project,
    Task LongestTaskByDescription,
    Task ShortestTaskByName,
    int? TeamMembersCount = null)
    {

    }
}
