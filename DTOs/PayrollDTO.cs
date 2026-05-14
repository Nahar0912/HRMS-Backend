using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Backend.DTOs
{
    public class PayrollDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal Tax { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayrollMonth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class PayrollCreateDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Tax { get; set; }

        [Required]
        public DateTime PayrollMonth { get; set; }
    }

    public class PayrollUpdateDTO
    {
        [Range(0, double.MaxValue)]
        public decimal? Tax { get; set; }
        public DateTime? PayrollMonth { get; set; }
    }
}
