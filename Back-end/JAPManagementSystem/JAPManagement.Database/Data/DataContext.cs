using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.SelectionModel;
using JAPManagement.Core.Models.StudentModel;
using JAPManagement.Core.Models.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace JAPManagement.Database.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            base.OnModelCreating(_modelBuilder);

            _modelBuilder.Entity<JapItem>()
            .HasMany(p => p.Programs)
            .WithMany(p => p.Items)
            .UsingEntity<ProgramItem>(
                j => j
                    .HasOne(pt => pt.Program)
                    .WithMany(t => t.ProgramItems)
                    .HasForeignKey(pt => pt.ProgramId),
                j => j
                    .HasOne(pt => pt.Item)
                    .WithMany(p => p.ProgramItems)
                    .HasForeignKey(pt => pt.ItemId),
                j =>
                {
                    j.HasKey(t => new { t.ItemId, t.ProgramId });
                });

            _modelBuilder.Entity<JapItem>()
            .HasMany(p => p.Students)
            .WithMany(p => p.Items)
            .UsingEntity<StudentItem>(
                j => j
                    .HasOne(pt => pt.Student)
                    .WithMany(t => t.StudentItems)
                    .HasForeignKey(pt => pt.StudentId),
                j => j
                    .HasOne(pt => pt.Item)
                    .WithMany(p => p.StudentItems)
                    .HasForeignKey(pt => pt.ItemId),
                j =>
                {
                    j.HasKey(t => new { t.ItemId, t.StudentId });
                });

            _modelBuilder.Entity<JapItem>().HasData(
                new JapItem
                {
                    Id = 1,
                    Name = "Modern React with Redux",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.dolor",
                    ExpectedHours = 25,
                    IsEvent = false
                },
                new JapItem
                {
                    Id = 2,
                    Name = "SQL Bootcamp",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.dolor",
                    ExpectedHours = 8,
                    IsEvent = false
                },
                new JapItem
                {
                    Id = 3,
                    Name = "Send completed assignment to mentor",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "",
                    ExpectedHours = 10,
                    IsEvent = true
                }, new JapItem
                {
                    Id = 4,
                    Name = "Postman API Testing",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.dolor",
                    ExpectedHours = 17,
                    IsEvent = false
                },
                new JapItem
                {
                    Id = 5,
                    Name = ".NET Core API | Jumpstart",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.dolor",
                    ExpectedHours = 12,
                    IsEvent = false
                },
                new JapItem
                {
                    Id = 6,
                    Name = "Project task no.1",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.org",
                    ExpectedHours = 12,
                    IsEvent = true
                }, new JapItem
                {
                    Id = 7,
                    Name = "HTML5 & CSS3 with Animations",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.dolor",
                    ExpectedHours = 13,
                    IsEvent = false
                },
                new JapItem
                {
                    Id = 8,
                    Name = "Complete Javascript Bootcamp | ES6",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "www.loremipsum.dolor",
                    ExpectedHours = 14,
                    IsEvent = false
                },
                new JapItem
                {
                    Id = 9,
                    Name = "Task refactor",
                    Description = "Lorem ipsum dolor sit amet",
                    URL = "",
                    ExpectedHours = 3,
                    IsEvent = true
                }
            );

            _modelBuilder.Entity<JapProgram>()
                .HasMany(p => p.Selections)
                .WithOne(s => s.JapProgram)
                .OnDelete(DeleteBehavior.Cascade);

            _modelBuilder.Entity<Selection>()
                .HasOne(s => s.JapProgram)
                .WithMany(p => p.Selections)
                .IsRequired(true);

            _modelBuilder.Entity<Student>()
                .HasOne(s => s.Selection)
                .WithMany(s => s.Students)
                .IsRequired(false);

            _modelBuilder.Entity<AdminReport>().ToTable("AdminReports", x => x.ExcludeFromMigrations())
        .HasNoKey();

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

            _modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = "10b5260a0-94c9-4681-8468-945a4aa4373f",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@hotmail.com",
                    NormalizedEmail = "JOHNDOE@HOTMAIL.COM",
                    NormalizedUserName = "JOHNDOE",
                    EmailConfirmed = false,
                    UserName = "johndoe",
                    PasswordHash = "AQAAAAEAACcQAAAAEAT9mk3FWTVJa/q7eobHLC7r4P8wMbs9fcfttAYtUF/7eGFX+sOtz9gosH5zWhNXiQ==",
                    SecurityStamp = "B4WFEMAOZ47PNHKJF642V6QWHWK2JHPN",
                    ConcurrencyStamp = "0af37133-6d9e-4e43-aa0a-e88240493840",
                    Role = UserRole.Student,
                    SelectionId = 1,
                    Status = StudentStatus.Success
                },
                new Student
                {
                    Id = "1023b5260a0-94c9-4681-8468-945a4aa4373f",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@hotmail.com",
                    NormalizedEmail = "JANEDOE@HOTMAIL.COM",
                    NormalizedUserName = "JANEDOE",
                    EmailConfirmed = false,
                    UserName = "janedoe",
                    PasswordHash = "AQAAAAEAACcQAAAAEAT9mk3FWTVJa/q7eobHLC7r4P8wMbs9fcfttAYtUF/7eGFX+sOtz9gosH5zWhNXiQ==",
                    SecurityStamp = "B4WFEMAOZ47PNHKJF642V6QWHWK2JHPN",
                    ConcurrencyStamp = "0af37133-6d9e-4e43-aa0a-e88240493840",
                    Role = UserRole.Student,
                    SelectionId = 2,
                    Status = StudentStatus.Failed
                },
                new Student
                {
                    Id = "1230b5260a0-94c9-4681-8468-945a4aa4373f",
                    FirstName = "Snoop",
                    LastName = "Dogg",
                    Email = "snoopdogg@hotmail.com",
                    NormalizedEmail = "SNOOPDOGG@HOTMAIL.COM",
                    NormalizedUserName = "SNOOPDOGG",
                    EmailConfirmed = false,
                    UserName = "snoopdogg",
                    PasswordHash = "AQAAAAEAACcQAAAAEAT9mk3FWTVJa/q7eobHLC7r4P8wMbs9fcfttAYtUF/7eGFX+sOtz9gosH5zWhNXiQ==",
                    SecurityStamp = "B4WFEMAOZ47PNHKJF642V6QWHWK2JHPN",
                    ConcurrencyStamp = "0af37133-6d9e-4e43-aa0a-e88240493840",
                    Role = UserRole.Student,
                    SelectionId = 1,
                    Status = StudentStatus.Failed
                });

            _modelBuilder.Entity<Admin>().HasData(
                 new Admin
                 {
                     Id = "0b5260a0-94c9-4681-8468-945a4aa4373f",
                     FirstName = "Malik",
                     LastName = "Huremovic",
                     Email = "malikhuremovic2001@hotmail.com",
                     NormalizedEmail = "MALIKHUREMOVIC2001@HOTMAIL.COM",
                     NormalizedUserName = "MALIKHUREM",
                     EmailConfirmed = false,
                     UserName = "malikhurem",
                     PasswordHash = "AQAAAAEAACcQAAAAEAT9mk3FWTVJa/q7eobHLC7r4P8wMbs9fcfttAYtUF/7eGFX+sOtz9gosH5zWhNXiQ==",
                     SecurityStamp = "B4WFEMAOZ47PNHKJF642V6QWHWK2JHPN",
                     ConcurrencyStamp = "0af37133-6d9e-4e43-aa0a-e88240493840",
                     Role = UserRole.Admin
                 });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<JapProgram> JapPrograms { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AdminReport> AdminReport { get; set; }
        public DbSet<JapItem> Items { get; set; }
        public DbSet<ProgramItem> ProgramItems { get; set; }
        public DbSet<StudentItem> StudentItems { get; set; }
    }
}