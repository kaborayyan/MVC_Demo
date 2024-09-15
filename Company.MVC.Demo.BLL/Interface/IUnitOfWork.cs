using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.BLL.Interface
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository iEmployeeRepository { get;}
        public IDepartmentRepository iDepartmentRepository { get;}
    }
}
