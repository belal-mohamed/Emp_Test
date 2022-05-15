using AutoMapper;
using Employees.src.Application.Common.Interfaces;
using Employees.src.Application.Exceptions;
using Employees.src.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.EditEmployee
{
    public class EditEmployeeCommand : IRequest<int>
    {
        public EditEmployeeVm EmployeeVm { get; set; }
    }

    public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EditEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {

            var empVm = request.EmployeeVm;

            var employee = await _context.Employees.FindAsync(empVm.Id);

            if (employee == null)
            {
                throw new NotFoundException(nameof(employee), request.EmployeeVm.Id);
            }

            _mapper.Map(request.EmployeeVm, employee);

            employee.Address = new Domain.ValueObjects.Address(empVm.Street, empVm.City, empVm.State, empVm.Country);

            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }

}
