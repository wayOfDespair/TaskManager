using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public int DepartmentId { get; set; }
        
        public Department Department { get; set; }
        public List<Task> Tasks { get; set; }

        public bool IsValid()
        {
            return Name != null && LastName != null;
        }

        public override string ToString()
        {
            return $"Employee #{EmployeeId} {Name} {LastName}. {Department.Name}";
        }
    }
}
