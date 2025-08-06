using FluentValidation;
using Microsoft.AspNetCore.Http;
using SchoolManagment.Core.Features.Instructors.Commands.Models;

namespace SchoolManagment.Core.Features.Instructors.Commands.Validation
{
    public class AddInstructorCommandValidator : AbstractValidator<AddInstructorCommand>
    {
        public AddInstructorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");

            RuleFor(x => x.Postion)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(100).WithMessage("Position cannot exceed 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.DID)
                .GreaterThan(0).WithMessage("A valid Department ID must be selected.");

            RuleFor(x => x.Image)
                .Must(BeAValidImage).When(x => x.Image != null)
                .WithMessage("Only .jpg, .jpeg, and .png image files are allowed.");
        }

        private bool BeAValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}
