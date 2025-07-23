using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Features.Users.Commands.Models;
using SchoolManagment.Core.Resources;
namespace SchoolManagment.Core.Features.Users.Commands.Validations
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.CurrentPassword)
                     .NotEmpty().WithMessage(localizer[SharedResourcesKeys.NotEmpty])
                     .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.NewPassword)
                     .NotEmpty().WithMessage(localizer[SharedResourcesKeys.NotEmpty])
                     .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.ConfirmPassword)
             .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");

        }
    }
}
