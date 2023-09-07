using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

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
                    Cost = 1000.0
                },
                new Work
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(-1),
                    HoursQuantity = 30,
                    HourlyRate = 30.0,
                    Cost = 900.0
                },
                new Work
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(-2),
                    HoursQuantity = 50,
                    HourlyRate = 20.0,
                    Cost = 1000.0
                }
            );
        }
    }
}
