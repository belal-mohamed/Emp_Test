using Employees.src.Application.Common.Interfaces;
using Employees.src.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Employee>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.FindAsync(request.Id);
        }

    }
}
