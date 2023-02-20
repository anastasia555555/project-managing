namespace CollectionsAndLinq.DAL.Entities.Task
{
    public record TaskWithPerfomer(
    int Id,
    string Name,
    string Description,
    int ProjectId,
    TaskState State,
    DateTime CreatedAt,
    DateTime? FinishedAt,
    User Perfomer)
    {

    }
}
