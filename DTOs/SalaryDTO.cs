using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Backend.DTOs
{
    public class SalaryDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }

    public class SalaryCreateDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal BasicSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Bonus { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public decimal Deduction { get; set; } = 0;

        [Required]
        public DateTime EffectiveFrom { get; set; }
    }

    public class SalaryUpdateDTO
    {
        [Range(0, double.MaxValue)]
        public decimal? BasicSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Bonus { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Deduction { get; set; }

        public DateTime? EffectiveFrom { get; set; }
    }
}
