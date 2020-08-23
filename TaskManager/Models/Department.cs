using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public List<Employee> Employees { get; set; }

        public bool IsValid()
        {
            return Name != null;
        }

        public override string ToString()
        {
            return $"Department #{DepartmentId} {Name}. Employees count {Employees.Count}";
        }
    }
}
