using Employees.src.Application.Employees.Commands.CreateEmployee;
using Employees.src.Application.Employees.Commands.EditEmployee;
using Employees.src.Application.Exceptions;
using Employees.src.Domain.Entities;
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
            EditEmployeeVm emp = new EditEmployeeVm()
            {
                Id = 100,
                City = "bb",
                Country = "dd",
                Name = "sr",
                Street = "fre",
                State = "rere"
            };

            var editEmployeeCommand = new EditEmployeeCommand()
            {
                EmployeeVm = emp
            };

            await FluentActions.Invoking(() => SendAsync(editEmployeeCommand))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldEditEmployee()
        {
            var CreateEmpVm = new CreateEmployeeVm()
            {
                City = "Alexandria",
                State = "Alexandria",
                Name = "Belal",
                Country = "Egypt",
                Street = "most4areen"
            };
            var createEmployeeCommand = new CreateEmployeeCommand()
            {
                EmployeeVm = CreateEmpVm
            };
            var empId = await SendAsync(createEmployeeCommand);




            var editEmpVm = new EditEmployeeVm()
            {
                Id = empId,
                City = "ss",
                State = "ss",
                Name = "ss",
                Country = "ss",
                Street = "ss"
            };

            await SendAsync(new EditEmployeeCommand()
            {
                EmployeeVm = editEmpVm
            });
            var editedEmp = await FindAsync<Employee>(empId);





            editedEmp.Name.Should().Be(editEmpVm.Name);
            editedEmp.Address.State.Should().Be(editEmpVm.State);
            editedEmp.Address.City.Should().Be(editEmpVm.City);
            editedEmp.Address.Country.Should().Be(editEmpVm.Country);
            editedEmp.Address.Street.Should().Be(editEmpVm.Street);

        }
    }
}
