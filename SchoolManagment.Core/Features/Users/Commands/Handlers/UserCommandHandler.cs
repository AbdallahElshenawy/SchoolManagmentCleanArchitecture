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
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>

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

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>("User Not Found");
            var newUser = mapper.Map(request, user);
            var result = await userManager.UpdateAsync(newUser);
            if (!result.Succeeded)
                return BadRequest<string>();
            return Success("User Updated Successfully");

        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>("User Not Found");
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest<string>("Delete User Failed");
            return Success("User Deleted Successfully");
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>("User Not Found");
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to change password";
                return BadRequest<string>(errorMessage);
            }
            return Success("Password Changed Successfully");

        }
    }
}