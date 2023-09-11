using System;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.DatabaseSeeding
{
    public class UserSeeder : IEntitySeeder
    {
        public void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(user => user.IsDeleted == false);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Pablo",
                    LastName = "Ortiz",
                    Dni = 1001010,
                    Type = 1,
                    Password = "123",
                    Email = "pablo@example.com", // Provide an email address
                    IsDeleted = false,
                    DeletedTimeUtc = DateTime.MinValue
                },
                new User
                {
                    Id = 2,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Dni = 213,
                    Type = 2,
                    Password = "456",
                    Email = "alice@example.com", // Provide an email address
                    IsDeleted = false,
                    DeletedTimeUtc = DateTime.MinValue
                },
                new User
                {
                    Id = 3,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Dni = 214,
                    Type = 1,
                    Password = "789",
                    Email = "bob@example.com", // Provide an email address
                    IsDeleted = true, // Soft-deleted
                    DeletedTimeUtc = DateTime.MinValue
                },
                new User
                {
                    Id = 4,
                    FirstName = "Eva",
                    LastName = "Lee",
                    Dni = 315,
                    Type = 2,
                    Password = "567",
                    Email = "eva@example.com", // Provide an email address
                    IsDeleted = false,
                    DeletedTimeUtc = DateTime.MinValue
                },
                new User
                {
                    Id = 5,
                    FirstName = "John",
                    LastName = "Doe",
                    Dni = 416,
                    Type = 1,
                    Password = "901",
                    Email = "john@example.com", // Provide an email address
                    IsDeleted = true, // Soft-deleted
                    DeletedTimeUtc = DateTime.MinValue
                }
            );
        }
    }
}
