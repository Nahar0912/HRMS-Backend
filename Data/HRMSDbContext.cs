using HRMS.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Backend.Data
{
    public class HRMSDbContext : DbContext
    {
        public HRMSDbContext(DbContextOptions<HRMSDbContext> options) : base(options) { } 
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Salary> Salaries { get; set; } = null!;
        public DbSet<Payroll> Payrolls { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasMany(e => e.Salaries)
                    .WithOne(s => s.Employee)
                    .HasForeignKey(s => s.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Payrolls)
                    .WithOne(p => p.Employee)
                    .HasForeignKey(p => p.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.AccountNumber).IsUnique();
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
