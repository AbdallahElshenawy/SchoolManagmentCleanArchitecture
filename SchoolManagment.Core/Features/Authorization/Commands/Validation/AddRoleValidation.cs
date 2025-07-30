using FluentValidation;
using SchoolManagment.Core.Features.Authorization.Commands.Models;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Authorization.Commands.Validation
{
    public class AddRoleValidation : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidation(IAuthorizationService authorizationService)
        {
            RuleFor(x => x.RoleName)
               .NotEmpty().NotNull().WithMessage("RoleName is required.");
            RuleFor(x => x.RoleName)
                .MustAsync(async (roleName, cancellation) => !await authorizationService.IsRoleExistByNameAsync(roleName))
                .WithMessage("Role already exists.");
        }
    }
}
