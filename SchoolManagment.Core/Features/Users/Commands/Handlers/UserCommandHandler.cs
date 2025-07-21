using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Users.Commands.Models;
using SchoolManagment.Core.Resources;
using SchoolManagment.Data.Entities.Identity;
namespace SchoolManagment.Core.Features.Departments.Queries.Handlers
{
    public class UserCommandHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager) : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>

    {
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest<string>(stringLocalizer[SharedResourcesKeys.EmailAlreadyExists]);
            }
            var userName = await userManager.FindByNameAsync(request.UserName);
            if (userName != null)
            {
                return BadRequest<string>(stringLocalizer[SharedResourcesKeys.UserNameAlreadyExists]);
            }
            var newUser = mapper.Map<User>(request);
            var result = await userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                return BadRequest<string>("Failed to create user");
            return Created("");
        }
    }
}
