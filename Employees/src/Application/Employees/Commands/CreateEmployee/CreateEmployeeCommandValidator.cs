using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {

            RuleFor(s => s.EmployeeVm)
                .NotNull()
                .WithMessage("Employee City Is Required");

            RuleFor(s => s.EmployeeVm.City)
                .NotEmpty()
                .WithMessage("Employee City Is Required");

            RuleFor(s => s.EmployeeVm.Country)
                .NotEmpty()
                .WithMessage("Employee Country Is Required");

            RuleFor(s => s.EmployeeVm.Name)
                .NotEmpty()
                .WithMessage("Employee Name Is Required")
                .MaximumLength(60);

            RuleFor(s => s.EmployeeVm.Street)
                .NotEmpty()
                .WithMessage("Employee Street Is Required");


        }
    }
}
