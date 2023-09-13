using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public class RoleSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Admin",
                    IsDeleted = false,

                },
                 new Role
                 {
                     Id = 2,
                     Name = "Consult",
                     Description = "Consult",
                     IsDeleted = false,
                 });
        }
    }
}
