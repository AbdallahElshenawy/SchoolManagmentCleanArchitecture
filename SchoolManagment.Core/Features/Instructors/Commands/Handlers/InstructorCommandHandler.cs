using AutoMapper;
using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Instructors.Commands.Models;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorCommandHandler(IMapper mapper, IInstructorService instructorService) : ResponseHandler,
        IRequestHandler<AddInstructorCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = mapper.Map<Instructor>(request);
            var result = await instructorService.AddInstructorAsync(instructor, request.Image);
            switch (result)
            {
                case "InstructorAdded":
                    return Success("Instructor added successfully.");
                case "FailedToAddInstructor":
                    return BadRequest<string>("Failed to add instructor.");
                case "FailedToUploadImage":
                    return BadRequest<string>("Failed to upload image.");
                case "NoImage":
                    return Success("Instructor added without an image.");
                default:
                    return BadRequest<string>("An unexpected error occurred.");
            }
        }
    }
}
