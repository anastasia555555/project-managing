namespace CollectionsAndLinq.DAL.Entities.Team
{
    public record TeamWithMembers(
    int Id,
    string Name,
    List<User> Members)
    {
    }
}
