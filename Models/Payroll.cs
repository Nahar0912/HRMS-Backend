using System.ComponentModel.DataAnnotations;

namespace HRMS.Backend.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public decimal TotalSalary { get; set; }   
        public decimal Tax { get; set; }
        public decimal NetSalary { get; set; }   

        [Required]
        public DateTime PayrollMonth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
