using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.BLL.Repository;
using Company.MVC.Demo.DAL.Models;
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

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            // Use GetAll() to get all the departments from the database
            var Departments = _departmentRepository.GetAll();
            return View(Departments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // This will work on the submit button
        [HttpPost]
        public IActionResult Create(Department department) // receive the data from the form
        {
            if (ModelState.IsValid)
            // Check the validity of data
            // Display the validation error messages
            {
                var count = _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index)); // or RedirectToAction("Index")
                }
            }
            return View(department); // to edit it again

        }
        #endregion

        #region Details
        public IActionResult Details(int? id) // Status 100
        {
            if (id == null) return BadRequest(); // Status 400
            var department = _departmentRepository.Get(id.Value);
            if (department == null) return NotFound(); // Status 404
            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            var department = _departmentRepository.Get(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id, Department department)
        {
            try
            {
                if (id != department.DepartmentId) return BadRequest();

                if (ModelState.IsValid)
                {
                    var count = _departmentRepository.Update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            
            return View(department);
        }
        #endregion

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            var department = _departmentRepository.Get(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            var count = _departmentRepository.Delete(department);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }
    }
}
