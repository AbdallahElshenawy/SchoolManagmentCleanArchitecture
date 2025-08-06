using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Instructors.Commands.Models;
using static SchoolManagment.Data.AppMetaData.Routing;

namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    public class InstructorController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost(InstructorRouting.Create)]
        public async Task<IActionResult> CreatInstructor([FromForm] AddInstructorCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
