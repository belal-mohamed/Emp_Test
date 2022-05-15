using Employees.src.Application.Common.Interfaces;
using Employees.src.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<IList<Employee>>
    {
    }

    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IList<Employee>>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IList<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync();
        }
    }
}
