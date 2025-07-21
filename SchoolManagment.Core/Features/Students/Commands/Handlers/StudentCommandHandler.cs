using AutoMapper;
using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Students.Commands.Models;
using SchoolManagment.Data.Entities;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler(IStudentService studentService, IMapper mapper) : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>

    {
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentDb = mapper.Map<Student>(request);
            var result = await studentService.AddStudentAsync(studentDb);

            return result switch
            {
                "Student already exists" => BadRequest<string>(result),
                "Student added successfully" => Created<string>(result),
                _ => BadRequest<string>("An error occurred while adding the student.")
            };
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentService.GetStudentByIdAsync(request.Id);
            if (student is null)
                return NotFound<string>("Student not found");
            var studentDb = mapper.Map<Student>(request);
            var result = await studentService.EditStudentAsync(studentDb);
            if (result == "Success")
                return Success<string>("Student updated sussessfully");
            else return BadRequest<string>(result);

        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentService.GetStudentByIdAsync(request.Id);
            if (student is null)
                return NotFound<string>("Student not found");
            var result = await studentService.DeleteStudentAsync(student);
            if (result == "Success")
                return Success<string>("Student Deleted sussessfully");
            else return BadRequest<string>(result);
        }
    }
}
