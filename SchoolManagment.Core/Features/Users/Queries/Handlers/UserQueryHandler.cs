using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Extensions;
using SchoolManagment.Core.Features.Users.Queries.Models;
using SchoolManagment.Core.Features.Users.Queries.Results;
using SchoolManagment.Core.Resources;
using SchoolManagment.Data.Entities.Identity;

namespace SchoolManagment.Core.Features.Users.Queries.Handlers;
public class UserQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager) : ResponseHandler,
   IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationResponse>>,
         IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
{
    public async Task<PaginatedResult<GetUserPaginationResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
    {
        var users = userManager.Users.AsQueryable();
        var paginatedList = await mapper.ProjectTo<GetUserPaginationResponse>(users)
                                        .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        return paginatedList;
    }

    public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user == null) return NotFound<GetUserByIdResponse>(stringLocalizer[SharedResourcesKeys.NotFound]);
        var result = mapper.Map<GetUserByIdResponse>(user);
        return Success(result);
    }
}