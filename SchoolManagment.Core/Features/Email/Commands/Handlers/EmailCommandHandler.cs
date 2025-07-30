using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Email.Commands.Models;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Email.Commands.Handlers
{
    public class EmailCommandHandler(IEmailService emailsService) : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await emailsService.SendEmail(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>("SendEmailFailed");
        }
    }
}
