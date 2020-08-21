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
    public class EmployeeController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: /employee/get/id?
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get(int? id)
        {
            return await _context.Employees.ToListAsync();
        }

        // POST: /employee/add
        [HttpPost]
        public async Task<IActionResult> Add(string jsonString)
        {
            if (jsonString == null || !JsonConvert.DeserializeObject<Employee>(jsonString).IsValid()) 
                return Ok("Invalid employee");
            
            var employee = JsonConvert.DeserializeObject<Employee>(jsonString);
            await _context.Employees.AddAsync(employee);
            
            return Ok();
        }
    }
}
