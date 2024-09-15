using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.DAL.Models
{
    public class Department: BaseModel
    {        
        [Required(ErrorMessage = "Department Code Is Required")]
        [DisplayName("Code")]
        public string DepartmentCode { get; set; }
        [Required(ErrorMessage = "Department Name Is Required")]
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
