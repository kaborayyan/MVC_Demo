using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.BLL.Repository;
using Company.MVC.Demo.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _departmentRepository = new DepartmentRepository(context);
            _employeeRepository = new EmployeeRepository(context);
        }
        public IEmployeeRepository iEmployeeRepository => _employeeRepository;
        // The original code
        // get
        //{
        // return _employeeRepository
        //}

        public IDepartmentRepository iDepartmentRepository => _departmentRepository;
    }
}
