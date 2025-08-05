using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authentication.Queries.Models;
using SchoolManagment.Core.Resources;
using SchoolManagment.Service.Abstracts;
namespace SchoolManagment.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
        IAuthenticationService authenticationService) : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>
    {
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (confirmEmail == "ErrorWhenConfirmEmail")
                return BadRequest<string>("ErrorWhenConfirmEmail");
            return Success<string>("ConfirmEmailDone");
        }
        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ConfirmResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>("UserIsNotFound");
                case "Failed": return BadRequest<string>("InvaildCode");
                case "Success": return Success<string>("");
                default: return BadRequest<string>("InvaildCode");
            }
        }
    }
}
