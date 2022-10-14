using JAPManagementSystem.Data;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.SelectionModel;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Models.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Emit;
using System.Text;

namespace JAPManagementSystem.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            base.OnModelCreating(_modelBuilder);
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
             );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<JapProgram> JapPrograms { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AdminReport> AdminReport { get; set; }
    }
}