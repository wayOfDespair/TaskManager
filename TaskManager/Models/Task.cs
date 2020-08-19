using System;

namespace TaskManager.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}