using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public class ServiceSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Description = "Service 1",
                    IsActive = true,
                    HourlyRate = 25.0
                },
                new Service
                {
                    Id = 2,
                    Description = "Service 2",
                    IsActive = true,
                    HourlyRate = 30.0
                },
                new Service
                {
                    Id = 3,
                    Description = "Service 3",
                    IsActive = false,
                    HourlyRate = 20.0
                }
            );
        }
    }
}