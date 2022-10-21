using JAPManagementSystem.Models;
using JAPManagementSystem.Models.SelectionModel;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Models.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

            _modelBuilder.Entity<JapProgram>().HasKey(j => j.Id);
            _modelBuilder.Entity<Item>().HasKey(l => l.Id);

            _modelBuilder.Entity<Item>()
            .HasMany(p => p.Programs)
            .WithMany(p => p.Items)
            .UsingEntity<ProgramItem>(
                j => j
                    .HasOne(pt => pt.Program)
                    .WithMany(t => t.ProgramItem)
                    .HasForeignKey(pt => pt.ProgramId),
                j => j
                    .HasOne(pt => pt.Item)
                    .WithMany(p => p.ProgramItem)
                    .HasForeignKey(pt => pt.ItemId),
                j =>
                {
                    j.HasKey(t => new { t.ItemId, t.ProgramId });
                });


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
        public DbSet<Item> Items { get; set; }
        public DbSet<ProgramItem> ProgramItems { get; set; }
    }
}