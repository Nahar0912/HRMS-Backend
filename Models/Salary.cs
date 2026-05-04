using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Backend.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } 
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public Employee? Employee { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
