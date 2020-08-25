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
    public class DepartmentController : Controller
    {
        private readonly DatabaseContext _context;

        public DepartmentController(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return departments collection.
        /// </summary>
        /// <returns></returns>
        
        // GET: api/department/
        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartmentsList()
        {
            return await _context.Departments.Include(d => d.Employees).ToListAsync();
        }
        
        /// <summary>
        /// Return a specific department by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // GET: api/department/id
        [HttpGet("{id}")]
        public async Task<Department> GetDepartment(int id)
        {
            return await _context.Departments
                .Include(department => department.Employees)
                .ThenInclude(employee => employee.Tasks)
                .SingleOrDefaultAsync(department => department.DepartmentId == id);
        }
        
        /// <summary>
        /// Create a new department.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        
        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            if (!department.IsValid()) return BadRequest("Invalid department");
            
            department.DepartmentId = await _context.Departments.CountAsync() + 1;
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            
            return Ok(HttpStatusCode.OK);
        }
        
        /// <summary>
        /// Edit an existing department by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentChanged"></param>
        /// <returns></returns>
        
        // PUT: api/department/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDepartment(int id, Department departmentChanged)
        {
            if (id < 0 || id > await _context.Departments.CountAsync())
                return BadRequest("Invalid id\n" + id);
            if (!departmentChanged.IsValid())
                return BadRequest("Invalid department\n" + departmentChanged);

            var department = await _context.Departments.FindAsync(id);
            department.Name = departmentChanged.Name;
            department.Employees = departmentChanged.Employees;
            
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            return Ok(HttpStatusCode.OK);
        }
        
        /// <summary>
        /// Delete a specific department by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // DELETE: api/department/id
        [HttpDelete("{id?}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            if (id < 0 || id > await _context.Departments.CountAsync())
                return BadRequest("Invalid id");
            
            var department = await _context.Departments.FindAsync(id);
            var employees = _context.Employees.Where(e => e.DepartmentId == id);
            foreach (var employee in employees)
            {
                employee.DepartmentId = 0;
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
