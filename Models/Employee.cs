namespace HRMS.Backend.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contact { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string EmploymentStatus { get; set; } = null!;
        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
    }
}
