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
                },
                new Project
                {
                    Id = 4,
                    Name = "Project 4",
                    Address = "101 Pine St",
                    Status = ProjectStatus.Confirmed
                },
                new Project
                {
                    Id = 5,
                    Name = "Project 5",
                    Address = "222 Cedar St",
                    Status = ProjectStatus.Pending
                },
                new Project
                {
                    Id = 6,
                    Name = "Project 6",
                    Address = "333 Oak St",
                    Status = ProjectStatus.Finished
                },
                new Project
                {
                    Id = 7,
                    Name = "Project 7",
                    Address = "444 Elm St",
                    Status = ProjectStatus.Pending
                },
                new Project
                {
                    Id = 8,
                    Name = "Project 8",
                    Address = "555 Maple St",
                    Status = ProjectStatus.Confirmed
                },
                new Project
                {
                    Id = 9,
                    Name = "Project 9",
                    Address = "666 Birch St",
                    Status = ProjectStatus.Pending
                },
                new Project
                {
                    Id = 10,
                    Name = "Project 10",
                    Address = "777 Walnut St",
                    Status = ProjectStatus.Pending
                }
            );
        }
    }
}
