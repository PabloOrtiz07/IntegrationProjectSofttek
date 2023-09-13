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
                    Password = PasswordEncryptHelper.EncryptPassword("123"),
                    Email = "adm", // Provide an email address
                    IsDeleted = false,
                    DeletedTimeUtc = null,
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Dni = 213,
                    Password = PasswordEncryptHelper.EncryptPassword("123"),
                    Email = "noAdmin", // Provide an email address
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
                    Password = PasswordEncryptHelper.EncryptPassword("1234"),
                    Email = "bob@example.com", // Provide an email address
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
                    Password = PasswordEncryptHelper.EncryptPassword("1234"),
                    Email = "eva@example.com", // Provide an email address
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
                    Password = PasswordEncryptHelper.EncryptPassword("1234"),
                    Email = "john@example.com", // Provide an email address
                    IsDeleted = true, // Soft-deleted
                    DeletedTimeUtc = DateTime.MinValue,
                    RoleId = 2
                }
            );
        }
    }
}
