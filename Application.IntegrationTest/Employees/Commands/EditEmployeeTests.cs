using DeepEqual.Syntax;
using Employees.src.Application.Employees.Commands.CreateEmployee;
using Employees.src.Application.Employees.Commands.EditEmployee;
using Employees.src.Application.Exceptions;
using Employees.src.Domain.Entities;
using Employees.src.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTest.Employees.Commands
{
    using static Testing;
    public class EditEmployeeTests : TestBase
    {

        [Test]
        public async Task ShouldRequireValidEmployeeId()
        {
            Employee emp = new Employee()
            {
                Id = 100,
                Name = "sr",
                Address = new Address 
                {
                    City = "bb",
                    Country = "dd",
                    Street = "fre",
                    State = "rere"
                
                }
            };

            var editEmployeeCommand = new EditEmployeeCommand()
            {
                Employee = emp
            };

            await FluentActions.Invoking(() => SendAsync(editEmployeeCommand))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldEditEmployee()
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
            var empId = await SendAsync(createEmployeeCommand);




            var editEmp = new Employee()
            {
                Id = empId,
                Name = "ss",
                Address = new Address()
                {
                    City = "ss",
                    State = "ss",
                    Country = "ss",
                    Street = "ss"
                }
            };

            await SendAsync(new EditEmployeeCommand()
            {
                Employee = editEmp
            });
            var editedEmp = await FindAsync<Employee>(empId);


            //editedEmp.ShouldDeepEqual(editEmp);

            bool result = editedEmp.IsDeepEqual(editEmp);

            result.Should().BeTrue();

            //editedEmp.Name.Should().Be(editEmpVm.Name);
            //editedEmp.Address.State.Should().Be(editEmpVm.State);
            //editedEmp.Address.City.Should().Be(editEmpVm.City);
            //editedEmp.Address.Country.Should().Be(editEmpVm.Country);
            //editedEmp.Address.Street.Should().Be(editEmpVm.Street);

        }
    }
}
