using CollectionsAndLinq.DAL.Entities.Project;
using CollectionsAndLinq.DAL.Entities.Team;
using CollectionsAndLinq.DAL.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.DAL.Context
{
    public class ThreadContext : DbContext
    {
        public DbSet<Project> Projects { get; private set; }
        public DbSet<Entities.Task.Task> Tasks { get; private set; }
        public DbSet<Team> Teams { get; private set; }
        public DbSet<User> Users { get; private set; }

        public ThreadContext(DbContextOptions<ThreadContext> options) : base(options)
        {

        }
    }
}
