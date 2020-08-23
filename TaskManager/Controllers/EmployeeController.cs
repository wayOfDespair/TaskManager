using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: /employee/id?
        [HttpGet("{id}")]
        public async Task<IEnumerable<Employee>> GetEmployeesList(int? id)
        {
            return id > 0 && id < await _context.Employees.CountAsync()
                ? await _context.Employees.Where(e => e.EmployeeId == id).ToListAsync()
                : await _context.Employees.ToListAsync();
        }

        // POST: /employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            if (!employee.IsValid())
                return BadRequest("Invalid employee");
            
            employee.EmployeeId = await _context.Employees.CountAsync(e => e.DepartmentId == employee.DepartmentId) + employee.DepartmentId * 1000;
            employee.Department = await _context.Departments.FindAsync(employee.DepartmentId);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        // PUT: /employee/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(int id, Employee employeeChanged)
        {
            if (id < 0 || id > await _context.Employees.CountAsync())
                return BadRequest("Invalid id\n" + id);
            if (!employeeChanged.IsValid())
                return BadRequest("Invalid employee\n" + employeeChanged);

            var employee = await _context.Employees.FindAsync(id);
            employee = employeeChanged;
            await _context.SaveChangesAsync();
            
            return Ok(HttpStatusCode.OK);
        }
        
        // DELETE: /employee/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            if (id < 0 || id > await _context.Employees.CountAsync())
                return BadRequest("Invalid id");

            var employee = await _context.Employees.FindAsync(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
