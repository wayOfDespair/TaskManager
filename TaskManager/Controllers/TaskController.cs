using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly DatabaseContext _context;

        public TaskController(DatabaseContext context)
        {
            _context = context;
        }
        
        // GET: /task/get/id?
        [HttpGet]
        public async Task<IEnumerable<Task>> Get(int? id)
        {
            return await _context.Tasks.ToListAsync();
        }

        
        // POST: /task/add
        [HttpPost]
        public async Task<IActionResult> Add(string jsonTask)
        {
            var task = (jsonTask != null) ? JsonConvert.DeserializeObject<Task>(jsonTask) : new Task();
            await _context.Tasks.AddAsync(task);
            
            return Ok("Task added.");
        }
    }
}
