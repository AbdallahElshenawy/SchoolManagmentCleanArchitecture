using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authorization.Commands.Models;
using SchoolManagment.Service.Abstracts;
namespace SchoolManagment.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler(IAuthorizationService authorizationService) : ResponseHandler, IRequestHandler<EditUserClaimsCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(EditUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.EditUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>("UserIsNotFound");
                case "FailedToRemoveOldClaims": return BadRequest<string>("FailedToRemoveOldClaims");
                case "FailedToAddNewClaims": return BadRequest<string>("FailedToAddNewClaims");
                case "FailedToUpdateClaims": return BadRequest<string>("FailedToUpdateClaims");
            }
            return Success<string>("Success");
        }
    }
}
