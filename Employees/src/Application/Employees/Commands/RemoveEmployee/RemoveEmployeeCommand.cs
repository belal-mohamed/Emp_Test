using Employees.src.Application.Common.Interfaces;
using Employees.src.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.RemoveEmployee
{
    public class RemoveEmployeeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);

            if (employee == null)
            {
                throw new NotFoundException(nameof(employee), request.Id);
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
