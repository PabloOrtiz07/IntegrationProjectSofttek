using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public class ProjectSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Name = "Project 1",
                    Address = "123 Main St",
                    Status = ProjectStatus.Pending
                },
                new Project
                {
                    Id = 2,
                    Name = "Project 2",
                    Address = "456 Elm St",
                    Status = ProjectStatus.Confirmed
                },
                new Project
                {
                    Id = 3,
                    Name = "Project 3",
                    Address = "789 Oak St",
                    Status = ProjectStatus.Pending
                }
            );
        }
    }
}
