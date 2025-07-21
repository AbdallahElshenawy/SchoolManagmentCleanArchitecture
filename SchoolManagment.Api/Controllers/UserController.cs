using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Users.Commands.Models;
using static SchoolManagment.Data.AppMetaData.Routing;
namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    public class UserController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost(UserRouting.Create)]
        public async Task<IActionResult> CreatStudent(AddUserCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }

    }
}
