using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authorization.Commands.Models;
using SchoolManagment.Core.Resources;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer)
        : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<EditUserRolesCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Role created successfully")
                return Success(result);
            return BadRequest<string>("Failed To Add Role");

        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.EditRoleAsync(request.Id, request.Name);
            if (result == "Role not found")
                return NotFound<string>("Role Not Found");
            else if (result == "Role updated successfully")
                return Success(result);
            else
                return BadRequest<string>(result);

        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.DeleteRoleAsync(request.Id);
            if (result == "Role not found")
                return NotFound<string>("Role Not Found");
            else if (result == "Role deleted successfully")
                return Success(result);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.EditUserRoles(request);
            switch (result)
            {
                case "User Not Found": return NotFound<string>(result);
                case "Failed To Remove Old Roles": return BadRequest<string>(result);
                case "Failed To Add New Roles": return BadRequest<string>(result);
            }
            return Success<string>(result);

        }
    }
}
