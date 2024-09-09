using Company.MVC.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.BLL.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department Get(int? id);
        
        // Remember SaveChanges() used to return a number
        // So each of the next methods can return a number
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
