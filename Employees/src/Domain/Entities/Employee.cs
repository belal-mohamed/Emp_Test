using Employees.src.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public string Image { get; set; }
        public Address Address { get; set; }

    }
}
