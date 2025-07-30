using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authorization.Queries.Models;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Results;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler(IAuthorizationService authorizationService, UserManager<User> userManager) : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResult>("UserIsNotFound");
            var result = await authorizationService.ManageUserClaimData(user);
            return Success(result);
        }
    }
}
