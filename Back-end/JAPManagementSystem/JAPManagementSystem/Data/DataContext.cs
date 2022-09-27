using JAPManagementSystem.Data;
using JAPManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JAPManagementSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            _modelBuilder.Entity<JapProgram>().HasData(
            new JapProgram
            {
                Id = 1,
                Name = "JAP DEV",
                Content = ".NET & React.js"
            },
            new JapProgram
            {
                Id = 2,
                Name = "JAP QA",
                Content = "Selenium & Unit & Integration Testing"
            },
            new JapProgram
            {
                Id = 3,
                Name = "JAP DevOps",
                Content = "Linux & Docker"
            });

            _modelBuilder.Entity<Selection>().HasData(
                new Selection
                {
                    Id = 1,
                    Name = "Dev Jap September",
                    DateStart = new DateTime(2022, 9, 5),
                    DateEnd = new DateTime(2022, 11, 5),
                    JapProgramId = 1,
                    Status = SelectionStatus.Active
                },
                new Selection
                {
                    Id = 2,                   
                    Name = "Dev QA June",
                    DateStart = new DateTime(2022, 6, 5),
                    DateEnd = new DateTime(2022, 8, 5),
                    JapProgramId = 2,
                    Status = SelectionStatus.Completed
                }
             ) ;


            _modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@mail.com",
                    Status = StudentStatus.InProgram,
                    SelectionId = 1,
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janendoe@mail.com",
                    Status = StudentStatus.Success,
                    SelectionId = 1

                }, new Student
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@mail.com",
                    Status = StudentStatus.InProgram,
                    SelectionId = 1,
                }, new Student
                {
                    Id = 4,
                    FirstName = "Malik",
                    LastName = "Huremović",
                    Email = "malikhuremovic01@mail.com",
                    Status = StudentStatus.InProgram,
                    SelectionId = 1,
                }, 
                new Student
                {
                    Id = 5,
                    FirstName = "Ishak",
                    LastName = "Isabegović",
                    Email = "isakIsabegovic@mail.com",
                    Status = StudentStatus.InProgram,
                    SelectionId = 1,
                },
                new Student
                {
                    Id = 6,
                    FirstName = "Emir",
                    LastName = "Bajrić",
                    Email = "emirbajric@mail.com",
                    Status = StudentStatus.Success,
                    SelectionId = 2,
                },
                new Student
                {
                    Id = 7,
                    FirstName = "Zlatan",
                    LastName = "Sprečo",
                    Email = "zlatansprečo@mail.com",
                    Status = StudentStatus.Success,
                    SelectionId = 2,
                },
                new Student
                {
                    Id = 8,
                    FirstName = "Sanel",
                    LastName = "Hodžić",
                    Email = "sanelh@mail.com",
                    Status = StudentStatus.Success,
                    SelectionId = 2,
                }
            );

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<JapProgram> JapPrograms { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}