
namespace CollectionsAndLinq.BL.Entities
{
    public record TeamWithMembers(
    int Id,
    string Name,
    List<User> Members)
    {
    }
}
