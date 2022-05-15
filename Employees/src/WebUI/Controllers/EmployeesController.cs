using Employees.src.Application.Employees.Commands.CreateEmployee;
using Employees.src.Application.Employees.Commands.EditEmployee;
using Employees.src.Application.Employees.Commands.RemoveEmployee;
using Employees.src.Application.Employees.Commands.UploadEmployeePhoto;
using Employees.src.Application.Employees.Queries.GetEmployees;
using Employees.src.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Employees.src.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiControllerBase
    {

        // Start Queries Apis
        [HttpGet]
        public async Task<ActionResult<IList<Employee>>> GetAllEmployees([FromQuery] GetEmployeesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await Mediator.Send(new GetEmployeeQuery() { Id = id });

            if (employee == null)
            {
                return NoContent();
            }

            return Ok(employee);
        }
        // End Queries Apis




        // Start Commands Apis
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee(EditEmployeeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] RemoveEmployeeCommand request)
        {
            await Mediator.Send(request);

            return NoContent();
        }

        [HttpPost]
        [Route("/UploadPhoto/{id}")]
        public async Task<IActionResult> UploadPhoto(int id)
        {

            var file = Request.Form.Files[0];
            var employee = await Mediator.Send(new GetEmployeeQuery() { Id = id });


            if (file.Length > 0 && employee != null)
            {
                return Ok(await Mediator.Send(new UploadEmployeePhotoCommand { File = file, Employee = employee }));
            }

            return BadRequest();

        }

        // End Commands Apis

    }
}
