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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        // Instead of opening and closing the connection each time
        // Create a private field of type AppDbContext you created before
        // _context default value is null
        // You have to create a constructor that will assign a value to _context
        // else you will get null exception once you start using the methods
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // The 5 basic methods
        public IEnumerable<T> GetAll()
        {
            // Set<T>() instead of Department or Employee
            return _context.Set<T>().ToList();
        }

        public T Get(int id)
        {
            // return _context.Departments.FirstOrDefault(D => D.DepartmentId == id);
            // The same as
            return _context.Set<T>().Find(id);
        }

        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges(); // SaveChanges() will return a number
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }
    }
}
