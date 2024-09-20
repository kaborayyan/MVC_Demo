using AutoMapper;
using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.DAL.Models;
using Company.MVC.Demo.Helpers;
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
        public async Task<IActionResult> Index(string searchInput)
        {
            var Employees = Enumerable.Empty<Employee>();
            // var EmployeesViewModel = new Collection<EmployeeViewModel>();
            // to use the ViewModel if you need
            if (string.IsNullOrEmpty(searchInput))
            {
                // Use GetAll() to get all the employees from the database
                //Employees = _EmployeeRepository.GetAll();
                Employees = await _unitOfWork.iEmployeeRepository.GetAllAsync();
            }
            else
            {
                //Employees = _EmployeeRepository.GetByName(searchInput);
                Employees = await _unitOfWork.iEmployeeRepository.GetByNameAsync(searchInput);
            }
            // Auto mapping
            var EmployeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(Employees);
            return View(EmployeesViewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // To enable department selection on the front end
            //var Departments = _DepartmentRepository.GetAll(); // Extra Data
            var Departments = await _unitOfWork.iDepartmentRepository.GetAllAsync(); // Extra Data
            ViewData["Departments"] = Departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            // Auto mapping
            var employee = _mapper.Map<Employee>(model);
            if (ModelState.IsValid)
            {
                // to map the uploaded image file
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                //var count = _EmployeeRepository.Add(employee);
                var count = await _unitOfWork.iEmployeeRepository.AddAsync(employee);
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
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id == null) return BadRequest();
            //var employee = _EmployeeRepository.Get(id.Value);
            var employee = await _unitOfWork.iEmployeeRepository.GetAsync(id.Value);
            if (employee == null) return NotFound();
            return View(viewName, employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //var Departments = _DepartmentRepository.GetAll(); // Extra Data
            var Departments = await _unitOfWork.iDepartmentRepository.GetAllAsync(); // Extra Data
            ViewData["Departments"] = Departments;
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                // If there's an image delete it then upload new image
                if (model.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                // Auto mapping
                var employee = _mapper.Map<Employee>(model);
                if (id != employee.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    //var count = _EmployeeRepository.Update(employee);
                    var count = await _unitOfWork.iEmployeeRepository.UpdateAsync(employee);
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                // Auto mapping
                var employee = _mapper.Map<Employee>(model);
                if (id != employee.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    //var count = _employeeRepository.Delete(employee);
                    var count = await _unitOfWork.iEmployeeRepository.DeleteAsync(employee);
                    if (count > 0)
                    {
                        // Delete the image from the server after the object has been deleted from the data base
                        DocumentSettings.DeleteFile(model.ImageName, "images");
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
