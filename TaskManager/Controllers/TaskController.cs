using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly DatabaseContext _context;

        public TaskController(DatabaseContext context)
        {
            _context = context;
        }
        
        // GET: /task/id?
        [HttpGet("{id?}")]
        public async Task<IEnumerable<Task>> GetTasksList(int? id)
        {
            return id > 0 && id < await _context.Employees.CountAsync()
                ? await _context.Tasks.Where(t => t.TaskId == id).ToListAsync()
                : await _context.Tasks.ToListAsync();
        }
        
        // POST: /task
        [HttpPost]
        public async Task<IActionResult> CreateTask(Task task)
        {
            if (!task.IsValid())
                return BadRequest($"Invalid task \n{task}");

            var lastTask = await _context.Tasks.OrderByDescending(t => t.TaskId).FirstAsync();
            task.TaskId = lastTask.TaskId + 1;
            task.Author = await _context.Employees.FindAsync(task.AuthorId);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        // PUT: /task/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, Task taskChanged)
        {
            if (id < 0 || id > await _context.Tasks.CountAsync())
                return BadRequest("Invalid id\n" + id);
            if (!taskChanged.IsValid())
                return BadRequest("Invalid task\n" + taskChanged);

            var task = await _context.Tasks.FindAsync(id);
            task = taskChanged;
            await _context.SaveChangesAsync();
            
            return Ok(HttpStatusCode.OK);
        }
        
        // DELETE: /task/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTask(int id)
        {
            if (id < 0 || id > await _context.Tasks.CountAsync())
                return BadRequest("Invalid id");

            var task = await _context.Tasks.FindAsync(id);
            _context.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
