using Company.MVC.Demo.BLL.Interface;
using Company.MVC.Demo.DAL.Data.Context;
using Company.MVC.Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
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
        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // The 5 basic methods
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                // Temporary solution to let EF load the navigation property
                return (IEnumerable<T>)await _context.Employees.Include(E => E.WorkFor).AsNoTracking().ToListAsync();
            }
            else
            {
                // Set<T>() instead of Department or Employee
                return await _context.Set<T>().ToListAsync();
            }            
        }

        public async Task<T> GetAsync(int id)
        {
            // return _context.Departments.FirstOrDefault(D => D.DepartmentId == id);
            // The same as
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync(); // SaveChanges() will return a number
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
