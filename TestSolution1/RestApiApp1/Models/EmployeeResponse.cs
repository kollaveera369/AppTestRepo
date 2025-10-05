namespace RestApiApp1.Models
{
    public class EmployeeResponse
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int Salary { get; set; }
    }
}