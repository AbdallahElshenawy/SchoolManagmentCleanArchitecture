using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Departments.Queries.Models;
using static SchoolManagment.Data.AppMetaData.Routing;

namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    public class DepartmentController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet(DepartmentRouting.GetDepartmentById)]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var response = await mediator.Send(new GetDepartmentByIdQuery(id));
            return NewResult(response);
        }
    }
}
