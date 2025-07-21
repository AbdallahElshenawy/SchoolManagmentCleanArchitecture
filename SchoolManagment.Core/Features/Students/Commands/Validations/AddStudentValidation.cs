using FluentValidation;
using SchoolManagment.Core.Features.Students.Commands.Models;
namespace SchoolManagment.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        public AddStudentValidation()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name can't be empety")
                .NotNull()
                .MaximumLength(10);

            RuleFor(s => s.Address).NotEmpty().WithMessage("Address can't be empety")
                .NotNull()
                .MaximumLength(10);
        }
    }
}
