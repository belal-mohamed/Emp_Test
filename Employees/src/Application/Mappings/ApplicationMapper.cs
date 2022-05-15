using AutoMapper;
using Employees.src.Application.Employees.Commands.CreateEmployee;
using Employees.src.Application.Employees.Commands.EditEmployee;
using Employees.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.src.Application.Mappings
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<CreateEmployeeVm, Employee>();
            CreateMap<Employee, Employee>();
            CreateMap<EditEmployeeVm, Employee>();
        }
    }
}
