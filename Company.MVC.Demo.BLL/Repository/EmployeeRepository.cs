using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.DAL.Data.Context;
using Company.MVC.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {        
        public EmployeeRepository(AppDbContext context):base(context)
        {            
        }

        #region Old Code
        /*
         public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return _context.Employees.Find(id);
        }

        public int Add(Employee entity)
        {
            _context.Employees.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Employee entity)
        {
            _context.Employees.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            return _context.SaveChanges();
        }
         */
        #endregion

    }
}
