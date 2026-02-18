using System.ComponentModel.DataAnnotations;

namespace HRMS.Backend.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string EmploymentStatus { get; set; } = null!;
    }

    public class EmployeeCreateDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, Phone]
        public string Contact { get; set; } = null!;

        [Required, StringLength(50)]
        public string Position { get; set; } = null!;

        [Required, StringLength(50)]
        public string Department { get; set; } = null!;

        [Required, StringLength(20)]
        public string AccountNumber { get; set; } = null!;

        [Required, StringLength(20)]
        public string EmploymentStatus { get; set; } = null!;
    }

    public class EmployeeUpdateDTO
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Contact { get; set; }

        [StringLength(50)]
        public string? Position { get; set; }

        [StringLength(50)]
        public string? Department { get; set; }

        [StringLength(20)]
        public string? AccountNumber { get; set; }

        [StringLength(20)]
        public string? EmploymentStatus { get; set; }
    }
}

