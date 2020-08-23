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

        // GET: /department/id?
        [HttpGet("{id?}")]
        public async Task<IEnumerable<Department>> GetDepartmentsList(int? id)
        {
            return id > 0 && id < await _context.Employees.CountAsync()
                ? await _context.Departments.Where(d => d.DepartmentId == id).ToListAsync()
                : await _context.Departments.ToListAsync();
        }
        
        // POST: /department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            if (!department.IsValid())
                return BadRequest("Invalid department");
            
            department.DepartmentId = await _context.Departments.CountAsync() + 1;
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            
            return Ok(HttpStatusCode.OK);
        }
        
        // PUT: /department/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDepartment(int id, Department departmentChanged)
        {
            if (id < 0 || id > await _context.Departments.CountAsync())
                return BadRequest("Invalid id\n" + id);
            if (!departmentChanged.IsValid())
                return BadRequest("Invalid department\n" + departmentChanged);

            var department = await _context.Departments.FindAsync(id);
            department = departmentChanged;
            await _context.SaveChangesAsync();

            return Ok(HttpStatusCode.OK);
        }
        
        // DELETE: /department/id
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
