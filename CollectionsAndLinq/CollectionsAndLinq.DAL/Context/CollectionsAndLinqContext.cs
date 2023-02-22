using CollectionsAndLinq.DAL.Entities.Project;
using CollectionsAndLinq.DAL.Entities.Team;
using CollectionsAndLinq.DAL.Entities.User;
using Microsoft.EntityFrameworkCore;
using Task = CollectionsAndLinq.DAL.Entities.Task.Task;

namespace CollectionsAndLinq.DAL.Context
{
    public class CollectionsAndLinqContext : DbContext
    {
        public DbSet<Project> Projects { get; private set; }
        public DbSet<Task> Tasks { get; private set; }
        public DbSet<Team> Teams { get; private set; }
        public DbSet<User> Users { get; private set; }

        public CollectionsAndLinqContext(DbContextOptions<CollectionsAndLinqContext> options) : base(options)
        {

        }
    }
}
