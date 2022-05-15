using Employees.src.Application.Employees.Commands.CreateEmployee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.RemoveEmployee
{
    public class RemoveEmployeeCommandValidator : AbstractValidator<RemoveEmployeeCommand>
    {
        public RemoveEmployeeCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotNull()
                .GreaterThanOrEqualTo(50);
        }
    }
}
