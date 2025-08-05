using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Authentication.Commands.Models;
using SchoolManagment.Core.Features.Authentication.Queries.Models;
using static SchoolManagment.Data.AppMetaData.Routing;

namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    public class AuthController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost(AuthRouting.SignIn)]
        public async Task<IActionResult> GenerateToken(SignInCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(AuthRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(AuthRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
        [HttpGet(AuthRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
        [HttpPost(AuthRouting.SendResetPasswordCode)]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(AuthRouting.ConfirmResetPasswordCode)]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
        {
            var response = await mediator.Send(query);
            return NewResult(response);
        }
        [HttpPost(AuthRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
