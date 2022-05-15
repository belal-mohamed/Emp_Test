using AutoMapper;
using Employees.src.Application.Common.Interfaces;
using Employees.src.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public CreateEmployeeVm EmployeeVm { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var empVm = request.EmployeeVm;

            var employee = _mapper.Map<Employee>(empVm);

            employee.Address = new Domain.ValueObjects.Address(empVm.Street,empVm.City,empVm.State,empVm.Country);

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id; 
        }
    }
}
