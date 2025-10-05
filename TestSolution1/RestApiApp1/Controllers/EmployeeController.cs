using Microsoft.AspNetCore.Mvc;
using RestApiApp1.Models;

namespace RestApiApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        // Hardcoded employee list (acts as our in-memory "database")
        private static List<Employee> employees = new List<Employee>
        {
            new Employee { EmpId = 10011, FirstName = "Veera", LastName = "Kolla", Department = "IT", Salary = 30000 },
            new Employee { EmpId = 10012, FirstName = "John", LastName = "Smith", Department = "HR", Salary = 28000 },
            new Employee { EmpId = 10013, FirstName = "Satya", LastName = "Brown", Department = "Finance", Salary = 32000 },
            new Employee { EmpId = 10014, FirstName = "Priya", LastName = "Lee", Department = "IT", Salary = 35000 },
            new Employee { EmpId = 10015, FirstName = "Dhana", LastName = "Jones", Department = "Marketing",Salary = 29000 }
        };

        // ✅ GET all employees
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(employees);
        }

        // ✅ GET employee by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = employees.FirstOrDefault(e => e.EmpId == id);
            if (emp == null)
                return NotFound(new { message = $"Employee with ID {id} not found." });

            return Ok(emp);
        }

        // ✅ POST - add new employee
        [HttpPost]
        public IActionResult Create([FromBody] EmployeeRequest employeeRequest)
        {
            if (employeeRequest == null || string.IsNullOrEmpty(employeeRequest.FirstName) || string.IsNullOrEmpty(employeeRequest.LastName))
                return BadRequest(new { message = "Invalid employee data." });

            var newEmployee = new Employee();
            newEmployee.EmpId = employees.Max(e => e.EmpId) + 1; // auto-generate next ID 
            newEmployee.FirstName = employeeRequest.FirstName;
            newEmployee.LastName = employeeRequest.LastName;
            newEmployee.Salary = employeeRequest.Salary;
            newEmployee.Department= employeeRequest.Department;
            employees.Add(newEmployee);

            return CreatedAtAction(nameof(GetById), new { id = newEmployee.EmpId }, newEmployee);
        }

        // ✅ PUT - update existing employee
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeRequest updatedEmployee)
        {
            var emp = employees.FirstOrDefault(e => e.EmpId == id);
            if (emp == null)
                return NotFound(new { message = $"Employee with ID {id} not found." });

            emp.FirstName = updatedEmployee.FirstName;
            emp.LastName = updatedEmployee.LastName;
            emp.Department = updatedEmployee.Department;
            emp.Salary = updatedEmployee.Salary;

            return Ok(emp);
        }

        // ✅ DELETE - remove employee
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = employees.FirstOrDefault(e => e.EmpId == id);
            if (emp == null)
                return NotFound(new { message = $"Employee with ID {id} not found." });

            employees.Remove(emp);
            return Ok(new { message = $"Employee with ID {id} deleted successfully." });
        }
    }
}
