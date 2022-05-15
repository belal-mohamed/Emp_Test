using Employees.src.Application.Common.Interfaces;
using Employees.src.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.src.Application.Employees.Commands.UploadEmployeePhoto
{
    public class UploadEmployeePhotoCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
        public Employee Employee { get; set; }
    }

    public class UploadEmployeePhotoCommandHandler : IRequestHandler<UploadEmployeePhotoCommand, string>
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IApplicationDbContext _context;

        public UploadEmployeePhotoCommandHandler(IWebHostEnvironment hostEnvironment, IApplicationDbContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }
        public async Task<string> Handle(UploadEmployeePhotoCommand request, CancellationToken cancellationToken)
        {

                var file = request.File;
                var folderName = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                var fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                request.Employee.Image = fileName;
                await _context.SaveChangesAsync(cancellationToken);

                return fileName;
            

            
        }
    }
}
