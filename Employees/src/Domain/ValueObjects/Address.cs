using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Domain.ValueObjects
{
    [Owned]
    public class Address
    {
        public String Street { get;  init; }
        public String City { get;  init; }
        public String State { get; init; }
        public String Country { get; init; }

        public Address() { }

        public Address(string street, string city, string state, string country)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
        }
    }
}
