using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authorization.Queries.Models;
using SchoolManagment.Core.Features.Authorization.Queries.Results;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Results;
using SchoolManagment.Service.Abstracts;


namespace SchoolManagment.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler(IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>

    {
        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await authorizationService.GetAllRolesAsync();
            var results = mapper.Map<List<GetRolesListResult>>(roles);
            return Success(results);

        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return NotFound<GetRoleByIdResult>("Role is not found");
            }
            var result = mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound<ManageUserRolesResult>("User not found");
            }
            var result = await authorizationService.GetUserRoles(user);
            return Success(result);
        }
    }
}
