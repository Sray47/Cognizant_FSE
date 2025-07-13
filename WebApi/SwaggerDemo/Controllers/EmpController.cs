using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "IT", Salary = 75000 },
            new Employee { Id = 2, Name = "Jane Smith", Department = "HR", Salary = 65000 },
            new Employee { Id = 3, Name = "Bob Johnson", Department = "Finance", Salary = 80000 }
        };

        // GET: api/Emp
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return _employees;
        }

        // GET: api/Emp/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();
                
            return employee;
        }

        // POST: api/Emp
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);
            
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        // PUT: api/Emp/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
                return BadRequest();
                
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
                return NotFound();
                
            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Salary = employee.Salary;
            
            return NoContent();
        }

        // DELETE: api/Emp/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();
                
            _employees.Remove(employee);
            
            return NoContent();
        }
    }

    // Using the same Employee class defined in EmployeeController.cs
}
