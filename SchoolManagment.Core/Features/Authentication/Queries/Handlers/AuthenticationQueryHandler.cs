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
        IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return Success(result);
            return Unauthorized<string>(stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }
    }
}
