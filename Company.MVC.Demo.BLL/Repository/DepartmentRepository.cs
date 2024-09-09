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
    public class DepartmentRepository : IDepartmentRepository
    {
        // Instead of opening and closing the connection each time
        // Create a private field of type AppDbContext you created before
        // _context default value is null
        // You have to create a constructor that will assign a value to _context
        // else you will get null exception once you start using the methods
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        // The 5 basic methods
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
        public Department Get(int? id)
        {
            // return _context.Departments.FirstOrDefault(D => D.DepartmentId == id);
            // The same as
            return _context.Departments.Find(id);
        }
        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            return _context.SaveChanges(); // SaveChanges() will return a number
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



    }
}
