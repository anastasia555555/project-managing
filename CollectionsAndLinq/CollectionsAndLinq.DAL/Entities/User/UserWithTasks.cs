namespace CollectionsAndLinq.DAL.Entities.User
{
    public record UserWithTasks(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime RegisteredAt,
    DateTime BirthDay,
    List<Task.Task> Tasks)
    {

    }
}
