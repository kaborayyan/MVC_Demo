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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {   
        public DepartmentRepository(AppDbContext context):base(context)
        {            
        }

        #region Old Code
        /*
         // The 5 basic methods
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
        public Department Get(int id)
        {
            
            return _context.Departments.Find(id);
        }
        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            return _context.SaveChanges(); 
        }

        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }

         */
        #endregion


    }
}
