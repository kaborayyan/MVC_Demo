using Company.MVC.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.BLL.Interface
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        // Original Code

        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity);

        // You can let the repositories implement the IGenericRepository directly
        // However it's better to leave it like this in case you needed to
        // add something special to each repository for example

        IEnumerable<Employee> GetByName(string name); // The search function
    }
}
