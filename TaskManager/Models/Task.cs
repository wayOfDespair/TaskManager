using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public int Priority { get; set; }
        public int Severity { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        [Required]
        public int? AuthorId { get; set; }
        
        public Employee Author { get; set; }

        public bool IsValid()
        {
            return Description != null && AuthorId != null;
        }

        public override string ToString()
        {
            return $"Task #{TaskId} {Description}, created by {Author} at {DateCreated}";
        }
    }
}
