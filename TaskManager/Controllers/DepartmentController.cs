using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        private readonly DatabaseContext _context;

        public DepartmentController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: /department/id?
        [HttpGet]
        public async Task<IEnumerable<Department>> Index(int? id)
        {
            return await _context.Departments.ToListAsync();
        }
        
        // POST: /department
        [HttpPost]
        public async Task<IActionResult> Index(Department department)
        {
            department.DepartmentId = await _context.Departments.CountAsync() + 1;
            if (!department.IsValid()) return Ok("Invalid department");
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            
            return Ok("Department added");
        }
    }
}
