using CollectionsAndLinq.DAL.Entities.Project;
using CollectionsAndLinq.DAL.Entities.Team;
using CollectionsAndLinq.DAL.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Task = CollectionsAndLinq.DAL.Entities.Task.Task;

namespace CollectionsAndLinq.DAL.Context
{
    public class CollectionsAndLinqContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users {  get; set; }

        public CollectionsAndLinqContext(DbContextOptions<CollectionsAndLinqContext> options) : base(options) { }

        public class CollectionsAndLinqContextFactory : IDesignTimeDbContextFactory<CollectionsAndLinqContext>
        {
            public CollectionsAndLinqContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CollectionsAndLinqContext>();
                
                //hardcoded since 
                optionsBuilder.UseSqlServer("Server=(localdb);Database=CollectionsAndLinqDatabase;Trusted_Connection=True;");

                return new CollectionsAndLinqContext(optionsBuilder.Options);
            }
        }
    }
}
