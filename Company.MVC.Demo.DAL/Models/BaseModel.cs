using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.Demo.DAL.Models
{
    public class BaseModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
    }
}
