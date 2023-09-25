using System;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Helper;
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
                    FirstName = "Pablo",
                    LastName = "Ortiz",
                    Dni = 1001010,
                    Email = "adm@gmail.com", // Provide an email address
                    Password = PasswordEncryptHelper.EncryptPassword("123", "adm@gmail.com"),
                    IsDeleted = false,
                    DeletedTimeUtc = null,
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Kevin",
                    LastName = "Johnson",
                    Dni = 213,
                    Email = "noadm@gmail.com", // Provide an email address
                    Password = PasswordEncryptHelper.EncryptPassword("123", "noadm@gmail.com"),
                    IsDeleted = false,
                    DeletedTimeUtc = null,
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Dni = 214,
                    Email = "bob@example.com", // Provide an email address
                    Password = PasswordEncryptHelper.EncryptPassword("123", "bob@example.com"),
                    IsDeleted = true, // Soft-deleted
                    DeletedTimeUtc = DateTime.MinValue,
                    RoleId = 1
                },
                new User
                {
                    Id = 4,
                    FirstName = "Eva",
                    LastName = "Lee",
                    Dni = 315,
                    Email = "eva@example.com", // Provide an email address
                    Password = PasswordEncryptHelper.EncryptPassword("123", "eva@example.com"),
                    IsDeleted = false,
                    DeletedTimeUtc = DateTime.MinValue,
                    RoleId = 2
                },
                new User
                {
                    Id = 5,
                    FirstName = "John",
                    LastName = "Doe",
                    Dni = 416,
                    Email = "john@example.com", // Provide an email address
                    Password = PasswordEncryptHelper.EncryptPassword("123", "john@example.com"),
                    IsDeleted = true, // Soft-deleted
                    DeletedTimeUtc = DateTime.MinValue,
                    RoleId = 2
                }
            );
        }
    }
}
