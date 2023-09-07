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
                    Adress = "123 Main St",
                    Status = 1
                },
                new Project
                {
                    Id = 2,
                    Name = "Project 2",
                    Adress = "456 Elm St",
                    Status = 2
                },
                new Project
                {
                    Id = 3,
                    Name = "Project 3",
                    Adress = "789 Oak St",
                    Status = 1
                }
            );
        }
    }
}