using CollectionsAndLinq.BL.Entities;
using CollectionsAndLinq.BL.Models.Tasks;

namespace CollectionsAndLinq.BL.Entities
{
    public record UserWithTasks(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime RegisteredAt,
    DateTime BirthDay,
    List<Task> Tasks)
    {

    }

}
