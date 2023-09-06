using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public class UserSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                     new User
                     {
                         Id = 1,
                         Name = "Pablo",
                         Dni = 212,
                         Type = 1,
                         Password = "123"
                     },
                    new User
                    {
                        Id = 2,
                        Name = "Alice",
                        Dni = 213,
                        Type = 2,
                        Password = "456"
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Bob",
                        Dni = 214,
                        Type = 1,
                        Password = "789"
                    }
                    );
        }
    }
}
