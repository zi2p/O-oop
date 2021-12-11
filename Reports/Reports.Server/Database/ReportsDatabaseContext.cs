using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.Server.Database
{
    public sealed class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EmployeeDTO> Employees { get; set; }

        public DbSet<TaskModelDTO> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<TaskModel>().HasOne(model => model.AssignedEmployee);
            base.OnModelCreating(modelBuilder);
        }
    }
}