using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        
        public int Priority { get; set; }
        public int Severity { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Employee Author { get; set; }
    }
}