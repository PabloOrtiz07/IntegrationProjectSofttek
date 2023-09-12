using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public class WorkSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Work>().HasData(
                new Work
                {
                    Id = 1,
                    Date = DateTime.Now,
                    HoursQuantity = 40,
                    HourlyRate = 25.0,
                    Cost = 1000.0,
                    Project = 1, 
                    Service = 1   
                },
                new Work
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(-1),
                    HoursQuantity = 30,
                    HourlyRate = 30.0,
                    Cost = 900.0,
                    Project = 2, 
                    Service = 2   
                },
                new Work
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(-2),
                    HoursQuantity = 50,
                    HourlyRate = 20.0,
                    Cost = 1000.0,
                    Project = 1, 
                    Service = 3   
                },
                new Work
                {
                    Id = 4,
                    Date = DateTime.Now.AddDays(-3),
                    HoursQuantity = 35,
                    HourlyRate = 28.0,
                    Cost = 980.0,
                    Project = 2, 
                    Service = 1   
                },
                new Work
                {
                    Id = 5,
                    Date = DateTime.Now.AddDays(-4),
                    HoursQuantity = 45,
                    HourlyRate = 22.0,
                    Cost = 990.0,
                    Project = 3,
                    Service = 2   
                }
            );
        }
    }
}
