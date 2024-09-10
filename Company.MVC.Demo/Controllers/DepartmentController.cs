using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.BLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Company.MVC.Demo.Controllers
{
    public class DepartmentController : Controller
    {
        // Create a private field of the needed IRepository
        // To be able to use its methods
        private readonly IDepartmentRepository _departmentRepository; // Should be null
        // you have to assign value to it through modifying the constructor
        // The CLR will create the object when needed
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            // Use GetAll() to get all the departments from the database
            var Departments = _departmentRepository.GetAll();
            return View(Departments);
        }
    }
}
