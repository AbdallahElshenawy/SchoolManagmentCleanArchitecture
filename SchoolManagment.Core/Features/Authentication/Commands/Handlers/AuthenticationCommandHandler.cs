using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authentication.Commands.Models;
using SchoolManagment.Core.Resources;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Results;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IStringLocalizer<SharedResources> stringLocalizer,
        IAuthenticationService authenticationService) : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return BadRequest<JwtAuthResult>("User not found.");
            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!user.EmailConfirmed)
                return BadRequest<JwtAuthResult>("Please confirm your email.");
            if (!result.Succeeded)
            {
                return BadRequest<JwtAuthResult>("Invalid user name or password.");
            }
            var accessToken = await authenticationService.GetJWTToken(user);
            return Success(accessToken);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = authenticationService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }
    }
}
