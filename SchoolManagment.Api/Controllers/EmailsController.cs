using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Email.Commands.Models;
using static SchoolManagment.Data.AppMetaData.Routing;

namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmailsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost(Emails.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
    }
}
