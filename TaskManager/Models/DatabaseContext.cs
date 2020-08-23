using System;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new {DepartmentId = 1, Name = "Development department"});
            modelBuilder.Entity<Employee>().HasData(new {EmployeeId = 1, Name = "Name", LastName = "Lastname", DepartmentId = 1});
            modelBuilder.Entity<Task>().HasData(new
            {
                TaskId = 1,
                Priority = 1,
                Severity = 1,
                Description = "Test task",
                DateCreated = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(2),
                AuthorId = 1
            });
        }
    }
}
