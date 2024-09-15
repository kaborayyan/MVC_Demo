using Company.MVC.Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.MVC.Demo.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Range(25, 60, ErrorMessage = "The age must be between 25 and 60.")]
        public int? Age { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public int? WorkForId { get; set; } // FK
        public Department? WorkFor { get; set; } // Navigational Property
    }
}
