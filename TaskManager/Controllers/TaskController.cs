using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        

        [HttpGet]
        public async Task<IEnumerable<Task>> Get(int? id)
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string jsonTask)
        {
            var task = (jsonTask != null) ? JsonConvert.DeserializeObject<Task>(jsonTask) : new Task();
            await _context.Tasks.AddAsync(task);
            
            return Ok("Task added.");
        }
    }
}
