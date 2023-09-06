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
        public DbSet<Service> Services { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
