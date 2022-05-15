using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeVm
    {

        public string Name { get; set; }
        public string Image { get; set; }

        public string Street { get;  set; }

        public string City { get;  set; }

        public string Country { get;  set; }

        public string State { get;  set; }
    }
}
