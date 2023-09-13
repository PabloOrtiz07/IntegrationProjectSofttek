using IntegratorSofttek.DataAccess.DatabaseSeeding;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions options) : base( options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seeders = new List<IEntitySeeder>
            {
                new UserSeeder(),
                new ProjectSeeder(),
                new ServiceSeeder(),
                new WorkSeeder(),
                new RoleSeeder(),
            };

            foreach (var seeder in seeders)
            {

                seeder.SeedDatabase(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);

        }
    }
}
