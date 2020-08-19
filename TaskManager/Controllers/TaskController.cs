using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly IList<Task> _tasks = new List<Task>
        {
            new Task {Id = 0, Author = "test author", Description = "test description", DateCreated = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(7)},
            new Task {Id = 1, Author = "despair", Description = "description", DateCreated = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(14)},
        };
        
        [HttpGet]
        public IEnumerable<Task> Get(int? id)
        {
            if (id != null && id > 0 && id < _tasks.Count)
            {
                return _tasks.Where(task => task.Id == id);
            }
            return _tasks;
        }

        [HttpPost]
        public IActionResult Post(string task)
        {
            if (task != null)
            {
                _tasks.Add(JsonConvert.DeserializeObject<Task>(task));
            }

            return Ok();
        }
    }
}
