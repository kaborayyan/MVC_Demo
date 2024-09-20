﻿using Company.MVC.Demo.BLL.Interface;
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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        // Implementation of the search function
        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.WorkFor).ToListAsync();
            // You must use Include() since EF doesn't load navigational properties by default
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
