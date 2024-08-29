using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/Employees
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_db.Employees.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _db.Employees.Find(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
            };
            
            _db.Employees.Add(employeeEntity);
            _db.SaveChanges();
            
            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeEntity = _db.Employees.Find(id);
            if (employeeEntity is null)
            {
                return NotFound();
            }
            employeeEntity.Name = updateEmployeeDto.Name;
            employeeEntity.Email = updateEmployeeDto.Email;
            employeeEntity.Phone = updateEmployeeDto.Phone;
            employeeEntity.Salary = updateEmployeeDto.Salary;
            
            _db.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employeeEntity = _db.Employees.Find(id);

            if (employeeEntity is null)
            {
                return NotFound();
            }
            
            _db.Employees.Remove(employeeEntity);
            
            _db.SaveChanges();

            return Ok(GetAllEmployees());
        }
    }
}
