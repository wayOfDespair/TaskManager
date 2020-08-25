using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Return employees collection.
        /// </summary>
        /// <returns></returns>

        // GET: api/employee/
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployeesList()
        {
            return await _context.Employees.Include(employee => employee.Tasks).ToListAsync();
        }
        
        /// <summary>
        /// Return a specific employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // GET: api/employee/id
        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees
                .Include(employee => employee.Tasks)
                .Include(employee => employee.Department)
                .SingleOrDefaultAsync(employee => employee.EmployeeId == id);
        }

        /// <summary>
        /// Create a new employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        
        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            if (!employee.IsValid() ||
                employee.DepartmentId > await _context.Departments.CountAsync() ||
                employee.DepartmentId < 0)
                return BadRequest("Invalid employee");
            
            employee.EmployeeId = await _context.Employees.CountAsync(e => e.DepartmentId == employee.DepartmentId) + employee.DepartmentId * 1000;
            employee.Department = await _context.Departments.FindAsync(employee.DepartmentId);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        /// <summary>
        /// Edit an existing employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeChanged"></param>
        /// <returns></returns>
        
        // PUT: api/employee/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(int id, Employee employeeChanged)
        {
            if (id < 0 || id > await _context.Employees.CountAsync())
                return BadRequest("Invalid id\n" + id);
            
            if (!employeeChanged.IsValid() ||
                employeeChanged.DepartmentId > await _context.Departments.CountAsync() ||
                employeeChanged.DepartmentId < 0)
                return BadRequest("Invalid employee\n" + employeeChanged);

            var employee = await _context.Employees.FindAsync(id);
            employee.Name = employeeChanged.Name;
            employee.LastName = employeeChanged.LastName;
            employee.DepartmentId = employeeChanged.DepartmentId;
            employee.Department = employee.Department;
            employee.Tasks = employeeChanged.Tasks;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            
            return Ok(HttpStatusCode.OK);
        }
        
        /// <summary>
        /// Delete a specific employee by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // DELETE: api/employee/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            if (id < 0 || 
                id > await _context.Employees.CountAsync())
                return BadRequest("Invalid id");

            var employee = await _context.Employees.FindAsync(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
