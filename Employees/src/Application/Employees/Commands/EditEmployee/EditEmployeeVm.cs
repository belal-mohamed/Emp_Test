using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.EditEmployee
{
    public class EditEmployeeVm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        public string State { get; set; }
    }
}
