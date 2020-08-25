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
    public class TaskController : Controller
    {
        private readonly DatabaseContext _context;

        public TaskController(DatabaseContext context)
        {
            _context = context;
        }
        
        
        /// <summary>
        /// Return tasks collection.
        /// </summary>
        /// <returns></returns>
        
        // GET: api/taskItem/
        [HttpGet]
        public async Task<IEnumerable<TaskItem>> GetTasksList()
        {
            return await _context.Tasks
                .Include(task => task.Author)
                .ToListAsync();
        }
        
        /// <summary>
        /// Return a specific task by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // GET: api/taskItem/id
        [HttpGet("{id}")]
        public async Task<TaskItem> GetTask(int id)
        {
            return await _context.Tasks
                .Include(taskItem => taskItem.Author)
                .SingleOrDefaultAsync(taskItem => taskItem.TaskId == id);
        }
        
        /// <summary>
        /// Create a new task.
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        
        // POST: api/taskItem
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem taskItem)
        {
            if (!taskItem.IsValid()) return  BadRequest($"Invalid taskItem \n{taskItem}");

            var lastTask = await _context.Tasks.OrderByDescending(t => t.TaskId).FirstAsync();
            taskItem.TaskId = lastTask.TaskId + 1;
            taskItem.Author = await _context.Employees.FindAsync(taskItem.AuthorId);
            await _context.Tasks.AddAsync(taskItem);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        
        /// <summary>
        /// Edit an existing task by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskItemChanged"></param>
        /// <returns></returns>
        /// 
        // PUT: api/taskItem/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, TaskItem taskItemChanged)
        {
            if (id < 0 || id > await _context.Tasks.CountAsync()) return BadRequest("Invalid id\n" + id);
            if (!taskItemChanged.IsValid()) return BadRequest("Invalid taskItem\n" + taskItemChanged);

            var task = await _context.Tasks.FindAsync(id);
            task.Description = taskItemChanged.Description;
            task.Priority = taskItemChanged.Priority;
            task.Severity = taskItemChanged.Severity;
            task.AuthorId = task.AuthorId;
            task.Author = taskItemChanged.Author;
            task.ExpirationDate = task.ExpirationDate;
            task.IsCompleted = taskItemChanged.IsCompleted;
            
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            
            return Ok(HttpStatusCode.OK);
        }
        
        /// <summary>
        /// Delete a specific task by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // DELETE: api/taskItem/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTask(int id)
        {
            if (id < 0 || id > await _context.Tasks.CountAsync()) return BadRequest("Invalid id");

            var task = await _context.Tasks.FindAsync(id);
            _context.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
