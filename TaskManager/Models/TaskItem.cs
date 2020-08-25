using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Priority { get; set; }
        
        [Required]
        public int Severity { get; set; }
        
        [Required]
        public bool IsCompleted { get; set; }
        
        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
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
            return $"TaskItem #{TaskId} {Description}, created by {Author} at {DateCreated}";
        }
    }
}
