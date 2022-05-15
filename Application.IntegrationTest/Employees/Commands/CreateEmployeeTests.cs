using Employees.src.Application.Employees.Commands.CreateEmployee;
using Employees.src.Domain.Entities;
using Employees.src.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Employees.Commands
{
    using static Testing;
    public class CreateEmployeeTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var createEmployeeCommand = new CreateEmployeeCommand()
            {
                Employee = new Employee()
                {
                    Name = "belal",
                    Address = new Address()
                    {
                        City = "ddd"
                    }
                }
            };


            await FluentActions.Invoking(() => SendAsync(createEmployeeCommand))
                .Should().ThrowAsync<FluentValidation.ValidationException>();

        }

        [Test]
        public async Task ShouldCreateEmployee()
        {
            var empAddress = new Address()
            {
                City = "Alexandria",
                State = "Alexandria",
                Country = "Egypt",
                Street = "most4areen"
            };

            var emp = new Employee()
            {
                Name = "Belal",
                Address = empAddress
            };

            var createEmployeeCommand = new CreateEmployeeCommand()
            {
                Employee = emp
            };

            var request = await SendAsync(createEmployeeCommand);

            var employee = await FindAsync<Employee>(request);

            employee.Should().NotBeNull();


        }
    }
}
