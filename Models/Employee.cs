using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        public string EmpAddress { get; set; }
        [Required]
        [ForeignKey("Department")]
        public int DeptID { get; set; }
        
        public virtual Department Department { get; set; }
    }
}
