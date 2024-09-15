using AutoMapper;
using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.DAL.Models;
using Company.MVC.Demo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace Company.MVC.Demo.Controllers
{
    public class EmployeeController : Controller
    {
        //The IUnitOfWork will do the job of the two repositories

        //private readonly IEmployeeRepository _EmployeeRepository;
        //private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfwork,
            IMapper mapper)
        {
            //_EmployeeRepository = employeeRepository;
            //_DepartmentRepository = departmentRepository;
            _unitOfWork = unitOfwork;
            _mapper = mapper;
        }

        #region Index
        public IActionResult Index(string searchInput)
        {
            var Employees = Enumerable.Empty<Employee>();
            // var EmployeesViewModel = new Collection<EmployeeViewModel>();
            // to use the ViewModel if you need
            if (string.IsNullOrEmpty(searchInput))
            {
                // Use GetAll() to get all the employees from the database
                //Employees = _EmployeeRepository.GetAll();
                Employees = _unitOfWork.iEmployeeRepository.GetAll();
            }
            else
            {
                //Employees = _EmployeeRepository.GetByName(searchInput);
                Employees = _unitOfWork.iEmployeeRepository.GetByName(searchInput);
            }
            // Auto mapping
            var EmployeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(Employees);
            return View(EmployeesViewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            // To enable department selection on the front end
            //var Departments = _DepartmentRepository.GetAll(); // Extra Data
            var Departments = _unitOfWork.iDepartmentRepository.GetAll(); // Extra Data
            ViewData["Departments"] = Departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            // Auto mapping
            var employee = _mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {
                //var count = _EmployeeRepository.Add(employee);
                var count = _unitOfWork.iEmployeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "An Employee was created successfully";
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
            //var employee = _EmployeeRepository.Get(id.Value);
            var employee = _unitOfWork.iEmployeeRepository.Get(id.Value);
            if (employee == null) return NotFound();
            return View(viewName, employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //var Departments = _DepartmentRepository.GetAll(); // Extra Data
            var Departments = _unitOfWork.iDepartmentRepository.GetAll(); // Extra Data
            ViewData["Departments"] = Departments;
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                // Auto mapping
                var employee = _mapper.Map<Employee>(model);
                if (id != employee.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    //var count = _EmployeeRepository.Update(employee);
                    var count = _unitOfWork.iEmployeeRepository.Update(employee);
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

            return View(model);
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
        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                // Auto mapping
                var employee = _mapper.Map<Employee>(model);
                if (id != employee.Id) return BadRequest();

                if (ModelState.IsValid)
                {                    
                    //var count = _employeeRepository.Delete(employee);
                    var count = _unitOfWork.iEmployeeRepository.Delete(employee);
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

            return View(model);
        }
        #endregion
    }
}
