namespace CollectionsAndLinq.DAL.Entities.Project
{
    public record FullProject(
    int Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime Deadline,
    List<TaskWithPerfomer> Tasks,
    User Author,
    Team Team)
    {

    }
}
