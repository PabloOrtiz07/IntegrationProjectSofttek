﻿// <auto-generated />
using System;
using IntegratorSofttek.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntegratorSofttek.Migrations
{
    [DbContext(typeof(ContextDB))]
    partial class ContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IntegratorSofttek.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("project_address");

                    b.Property<DateTime?>("DeletedTimeUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("project_deletedTimeUtc");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("project_isDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("project_name");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("project_status");

                    b.HasKey("Id");

                    b.ToTable("projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Main St",
                            IsDeleted = false,
                            Name = "Project 1",
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            Address = "456 Elm St",
                            IsDeleted = false,
                            Name = "Project 2",
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            Address = "789 Oak St",
                            IsDeleted = false,
                            Name = "Project 3",
                            Status = 0
                        });
                });

            modelBuilder.Entity("IntegratorSofttek.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DeletedTimeUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("role_deletedTimeUtc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role_description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("role_isDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role_name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrator",
                            IsDeleted = false,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Consultant",
                            IsDeleted = false,
                            Name = "Consultant"
                        });
                });

            modelBuilder.Entity("IntegratorSofttek.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("service_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DeletedTimeUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("service_deletedTimeUtc");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("service_description");

                    b.Property<double>("HourlyRate")
                        .HasColumnType("float")
                        .HasColumnName("service_hourlyRate");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("service_isActive");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("service_isDeleted");

                    b.HasKey("Id");

                    b.ToTable("services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Service 1",
                            HourlyRate = 25.0,
                            IsActive = true,
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 2,
                            Description = "Service 2",
                            HourlyRate = 30.0,
                            IsActive = true,
                            IsDeleted = false
                        },
                        new
                        {
                            Id = 3,
                            Description = "Service 3",
                            HourlyRate = 20.0,
                            IsActive = false,
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("IntegratorSofttek.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DeletedTimeUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("user_deletedTimeUtc");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("user_dni");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("user_email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("user_firstName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("user_isDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("user_lastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("user_password");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Dni = "1001010",
                            Email = "adm@gmail.com",
                            FirstName = "Pablo",
                            IsDeleted = false,
                            LastName = "Ortiz",
                            Password = "9f3d321cd0a1ccafa899226d5190f74618cb23b789aa998e1d7f741956132434",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Dni = "213",
                            Email = "noadm@gmail.com",
                            FirstName = "Kevin",
                            IsDeleted = false,
                            LastName = "Johnson",
                            Password = "a10ad3a74bccd29b56cb5ec5a213d1a27b293b6bb88797418a31f09c2a707bf4",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            DeletedTimeUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dni = "214",
                            Email = "bob@example.com",
                            FirstName = "Bob",
                            IsDeleted = true,
                            LastName = "Smith",
                            Password = "a10ad3a74bccd29b56cb5ec5a213d1a27b293b6bb88797418a31f09c2a707bf4",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 4,
                            DeletedTimeUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dni = "315",
                            Email = "eva@example.com",
                            FirstName = "Eva",
                            IsDeleted = false,
                            LastName = "Lee",
                            Password = "ff192a780cb98e260d87c38683c2e155dfe48897e454e47390063fd76755651f",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 5,
                            DeletedTimeUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dni = "416",
                            Email = "john@example.com",
                            FirstName = "John",
                            IsDeleted = true,
                            LastName = "Doe",
                            Password = "fbafa90f00f6416a6d1e8535234f9603aaf07258d7a98424ec011a5f7aa634ff",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("IntegratorSofttek.Entities.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("work_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Cost")
                        .HasColumnType("float")
                        .HasColumnName("work_cost");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("work_date");

                    b.Property<DateTime?>("DeletedTimeUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("work_deletedTimeUtc");

                    b.Property<double>("HourlyRate")
                        .HasColumnType("float")
                        .HasColumnName("work_hourlyRate");

                    b.Property<int>("HoursQuantity")
                        .HasColumnType("int")
                        .HasColumnName("work_hoursQuantity");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("work_isDeleted");

                    b.Property<int>("Project")
                        .HasColumnType("int");

                    b.Property<int>("Service")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("works");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 1000.0,
                            Date = new DateTime(2023, 9, 22, 11, 55, 9, 382, DateTimeKind.Local).AddTicks(8463),
                            HourlyRate = 25.0,
                            HoursQuantity = 40,
                            IsDeleted = false,
                            Project = 1,
                            Service = 1
                        },
                        new
                        {
                            Id = 2,
                            Cost = 900.0,
                            Date = new DateTime(2023, 9, 21, 11, 55, 9, 382, DateTimeKind.Local).AddTicks(8473),
                            HourlyRate = 30.0,
                            HoursQuantity = 30,
                            IsDeleted = false,
                            Project = 2,
                            Service = 2
                        },
                        new
                        {
                            Id = 3,
                            Cost = 1000.0,
                            Date = new DateTime(2023, 9, 20, 11, 55, 9, 382, DateTimeKind.Local).AddTicks(8478),
                            HourlyRate = 20.0,
                            HoursQuantity = 50,
                            IsDeleted = false,
                            Project = 1,
                            Service = 3
                        },
                        new
                        {
                            Id = 4,
                            Cost = 980.0,
                            Date = new DateTime(2023, 9, 19, 11, 55, 9, 382, DateTimeKind.Local).AddTicks(8479),
                            HourlyRate = 28.0,
                            HoursQuantity = 35,
                            IsDeleted = false,
                            Project = 2,
                            Service = 1
                        },
                        new
                        {
                            Id = 5,
                            Cost = 990.0,
                            Date = new DateTime(2023, 9, 18, 11, 55, 9, 382, DateTimeKind.Local).AddTicks(8480),
                            HourlyRate = 22.0,
                            HoursQuantity = 45,
                            IsDeleted = false,
                            Project = 3,
                            Service = 2
                        });
                });

            modelBuilder.Entity("IntegratorSofttek.Entities.User", b =>
                {
                    b.HasOne("IntegratorSofttek.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
