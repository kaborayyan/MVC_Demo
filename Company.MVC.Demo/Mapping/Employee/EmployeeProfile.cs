using AutoMapper;
using Company.MVC.Demo.DAL.Models;
using Company.MVC.Demo.ViewModels;

namespace Company.MVC.Demo.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
        }
    }
}
