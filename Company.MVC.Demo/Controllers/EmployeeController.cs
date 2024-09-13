using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.MVC.Demo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            // Use GetAll() to get all the employees from the database
            var Employees = _employeeRepository.GetAll();
            return View(Employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)            
            {
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);

        }
        #endregion

        #region Details
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id == null) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null) return NotFound();
            return View(viewName, employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {            
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee employee)
        {            
            try
            {
                if (id != employee.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var count = _employeeRepository.Update(employee);
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

            return View(employee);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Employee employee)
        {
            try
            {
                if (id != employee.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var count = _employeeRepository.Delete(employee);
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

            return View(employee);
        }
        #endregion
    }
}
