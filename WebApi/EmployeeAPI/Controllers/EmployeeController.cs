using EmployeeAPI.Models;
using EmployeeAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthFilter]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>();

        public EmployeeController()
        {
            // Initialize with a few records if the list is empty
            if (_employees.Count == 0)
            {
                _employees = GetStandardEmployeeList();
            }
        }

        // GET: api/Employee
        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            // Throwing an exception for testing our exception filter
            if (DateTime.Now.Second % 5 == 0)
            {
                throw new Exception("This is a test exception to demonstrate the exception filter");
            }
            
            return Ok(_employees);
        }

        // GET: api/Employee/standard
        [HttpGet("standard")]
        [ProducesResponseType(typeof(Employee), 200)]
        public ActionResult<Employee> GetStandard()
        {
            // Return the first employee from the standard list
            if (_employees.Count > 0)
                return Ok(_employees[0]);
            
            return NotFound();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();
                
            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        [ProducesResponseType(typeof(Employee), 201)]
        [ProducesResponseType(400)]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);
            
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
                return BadRequest();
                
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
                return NotFound();
                
            // Update employee properties
            existingEmployee.Name = employee.Name;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.Permanent = employee.Permanent;
            existingEmployee.Department = employee.Department;
            existingEmployee.Skills = employee.Skills;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();
                
            _employees.Remove(employee);
            
            return NoContent();
        }

        // Private method to return a standard list of employees
        private List<Employee> GetStandardEmployeeList()
        {
            // Create department instances
            var itDepartment = new Department { Id = 1, Name = "IT" };
            var hrDepartment = new Department { Id = 2, Name = "HR" };
            var financeDepartment = new Department { Id = 3, Name = "Finance" };

            // Create skill instances
            var csharpSkill = new Skill { Id = 1, Name = "C#", Level = 5 };
            var sqlSkill = new Skill { Id = 2, Name = "SQL", Level = 4 };
            var jsSkill = new Skill { Id = 3, Name = "JavaScript", Level = 3 };
            var pythonSkill = new Skill { Id = 4, Name = "Python", Level = 4 };
            var excelSkill = new Skill { Id = 5, Name = "Excel", Level = 5 };

            // Create and return a list of employees
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Salary = 75000,
                    Permanent = true,
                    Department = itDepartment,
                    Skills = new List<Skill> { csharpSkill, sqlSkill, jsSkill },
                    DateOfBirth = new DateTime(1985, 5, 15)
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Salary = 65000,
                    Permanent = true,
                    Department = hrDepartment,
                    Skills = new List<Skill> { excelSkill },
                    DateOfBirth = new DateTime(1990, 10, 8)
                },
                new Employee
                {
                    Id = 3,
                    Name = "Bob Johnson",
                    Salary = 80000,
                    Permanent = false,
                    Department = financeDepartment,
                    Skills = new List<Skill> { sqlSkill, excelSkill },
                    DateOfBirth = new DateTime(1982, 3, 23)
                }
            };
        }
    }
}
