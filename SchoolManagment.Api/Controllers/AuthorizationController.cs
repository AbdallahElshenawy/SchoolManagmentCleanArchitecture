using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Authorization.Commands.Models;
using SchoolManagment.Core.Features.Authorization.Queries.Models;
using static SchoolManagment.Data.AppMetaData.Routing;

namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost(AuthorizationRouting.Create)]
        public async Task<IActionResult> CreatRole(AddRoleCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(AuthorizationRouting.Edit)]
        public async Task<IActionResult> EditRole(EditRoleCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(AuthorizationRouting.Delete)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var response = await mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet(AuthorizationRouting.GetRolesList)]
        public async Task<IActionResult> GetRolesList()
        {
            var response = await mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [HttpGet(AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var response = await mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);
        }
        [HttpGet(AuthorizationRouting.GetUserRoles)]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var response = await mediator.Send(new ManageUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }
        [HttpPut(AuthorizationRouting.EditUserRoles)]
        public async Task<IActionResult> EditUserRoles(EditUserRolesCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var response = await mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }


    }
}
