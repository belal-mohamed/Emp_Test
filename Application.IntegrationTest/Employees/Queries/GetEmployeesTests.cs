using Employees.src.Application.Employees.Queries.GetEmployees;
using Employees.src.Domain.Entities;
using Employees.src.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Employees.Queries
{
    using static Testing;
    public class GetEmployeesTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllEmployees()
        {
            await AddAsync(new Employee()
            {
                Name = "Belal",
                Address = new Address("elmost4areen", "alex", "alex", "egypt")

            });

            var query = new GetEmployeesQuery();

            var result = await SendAsync(query);

            result.Should().HaveCount(1);
        }
    }
}
