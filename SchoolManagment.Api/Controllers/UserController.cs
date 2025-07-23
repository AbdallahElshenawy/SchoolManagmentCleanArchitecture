using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Users.Commands.Models;
using SchoolManagment.Core.Features.Users.Queries.Models;
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
        [HttpGet(UserRouting.paginatedList)]
        public async Task<IActionResult> PaginatedList([FromQuery] GetUserPaginationQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(UserRouting.GetUserById)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }
        [HttpPut(UserRouting.Edit)]
        public async Task<IActionResult> EditStudent(EditUserCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete(UserRouting.Delete)]
        public async Task<IActionResult> DeleteStudentById(string id)
        {
            var response = await mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }
    }
}