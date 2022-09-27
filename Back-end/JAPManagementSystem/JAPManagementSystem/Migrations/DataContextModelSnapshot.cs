﻿// <auto-generated />
using System;
using JAPManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JAPManagementSystem.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("JAPManagementSystem.Models.JapProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JapPrograms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = ".NET & React.js",
                            Name = "JAP DEV"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Selenium & Unit & Integration Testing",
                            Name = "JAP QA"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Linux & Docker",
                            Name = "JAP DevOps"
                        });
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Selection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JapProgramId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JapProgramId");

                    b.ToTable("Selections");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateEnd = new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2022, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            JapProgramId = 1,
                            Name = "Dev Jap September",
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            DateEnd = new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            JapProgramId = 2,
                            Name = "Dev QA June",
                            Status = 2
                        });
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SelectionId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SelectionId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "johndoe@mail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            SelectionId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "janendoe@mail.com",
                            FirstName = "Jane",
                            LastName = "Doe",
                            SelectionId = 1,
                            Status = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "johndoe@mail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            SelectionId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 4,
                            Email = "malikhuremovic01@mail.com",
                            FirstName = "Malik",
                            LastName = "Huremović",
                            SelectionId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 5,
                            Email = "isakIsabegovic@mail.com",
                            FirstName = "Ishak",
                            LastName = "Isabegović",
                            SelectionId = 1,
                            Status = 1
                        },
                        new
                        {
                            Id = 6,
                            Email = "emirbajric@mail.com",
                            FirstName = "Emir",
                            LastName = "Bajrić",
                            SelectionId = 2,
                            Status = 2
                        },
                        new
                        {
                            Id = 7,
                            Email = "zlatansprečo@mail.com",
                            FirstName = "Zlatan",
                            LastName = "Sprečo",
                            SelectionId = 2,
                            Status = 2
                        },
                        new
                        {
                            Id = 8,
                            Email = "sanelh@mail.com",
                            FirstName = "Sanel",
                            LastName = "Hodžić",
                            SelectionId = 2,
                            Status = 2
                        });
                });

            modelBuilder.Entity("JAPManagementSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Comment", b =>
                {
                    b.HasOne("JAPManagementSystem.Models.Student", "Student")
                        .WithMany("Comments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Selection", b =>
                {
                    b.HasOne("JAPManagementSystem.Models.JapProgram", "JapProgram")
                        .WithMany()
                        .HasForeignKey("JapProgramId");

                    b.Navigation("JapProgram");
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Student", b =>
                {
                    b.HasOne("JAPManagementSystem.Models.Selection", "Selection")
                        .WithMany("Students")
                        .HasForeignKey("SelectionId");

                    b.Navigation("Selection");
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Selection", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("JAPManagementSystem.Models.Student", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
