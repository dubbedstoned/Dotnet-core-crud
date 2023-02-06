using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        [Key]
        public int DeptID { get; set; }
        public string DeptName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
