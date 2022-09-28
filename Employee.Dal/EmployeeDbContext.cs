using Microsoft.EntityFrameworkCore;
using Emp=Employee.Models.Entities;

namespace Employee.Dal
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext()
        {

        }
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Emp.Employee> Employees { get; set; }
        public DbSet<Emp.Event> Events { get; set; }
        public DbSet<Emp.Role> Roles { get; set; }
        public DbSet<Emp.User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source = localhost\\SQLEXPRESS; Initial Catalog = WebApiDb; Integrated Security = true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emp.Employee>().HasData(
                new Emp.Employee() { EmployeeId = 1, EmployeeName = "John Mark", Address = "East Wing, Z/101", City = "London", Country = "United Kingdom", Zipcode = "473837", Phone = "+044 73783783", Email = "john.mark@webpochub.com", Skillsets = "DBA", Avatar = "/images/john-mark.png" },
                new Emp.Employee() { EmployeeId = 2, EmployeeName = "Alisha C.", Address = "North Wing, Moon-01", City = "Mumbai", Country = "India", Zipcode = "367534", Phone = "+91 7865678645", Email = "alisha.c@webpochub.com", Skillsets = "People Management", Avatar = "/images/alisha-c.png" },
                new Emp.Employee() { EmployeeId = 3, EmployeeName = "Pravinkumar Dabade", Address = "Suncity, A8/404", City = "Pune", Country = "India", Zipcode = "411051", Phone = "+044 73783783", Email = "dabade.pravinkumar@webpochub.com", Skillsets = "Trainer & Consultant", Avatar = "/images/dabade-pravinkumar.png" }
            );
            modelBuilder.Entity<Emp.Role>().HasData(
                new Emp.Role() { RoleId = 1, RoleName = "Employee", RoleDescription = "Employee of WebPocHub Organization!" },
                new Emp.Role() { RoleId = 2, RoleName = "Hr", RoleDescription = "Hr of WebPocHub Organization!" }
            );
        }
    }
}