using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public List<Task> Tasks { get; set; }

        public bool IsValid()
        {
            return Name != null && LastName != null;
        }
    }
}
