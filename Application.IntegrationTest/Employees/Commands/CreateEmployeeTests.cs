using Employees.src.Application.Employees.Commands.CreateEmployee;
using Employees.src.Domain.Entities;
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
                EmployeeVm = new CreateEmployeeVm()
            };


            await FluentActions.Invoking(() => SendAsync(createEmployeeCommand))
                .Should().ThrowAsync<FluentValidation.ValidationException>();

        }

        [Test]
        public async Task ShouldCreateEmployee()
        {
            var empVm = new CreateEmployeeVm()
            {
                City = "Alexandria",
                State = "Alexandria",
                Name = "Belal",
                Country = "Egypt",
                Street = "most4areen"
            };


            var createEmployeeCommand = new CreateEmployeeCommand()
            {
                EmployeeVm = empVm
            };

            var request = await SendAsync(createEmployeeCommand);

            var employee = await FindAsync<Employee>(request);

            employee.Should().NotBeNull();


        }
    }
}
