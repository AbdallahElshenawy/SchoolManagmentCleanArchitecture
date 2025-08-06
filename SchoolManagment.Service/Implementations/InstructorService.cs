using Microsoft.AspNetCore.Http;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class InstructorService(IFileService fileService, IInstructorRepository instructorRepository) : IInstructorService
    {
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var imageUrl = await fileService.UploadImage("instructors", file);
            instructor.Image = imageUrl;
            switch (imageUrl)
            {
                case "FailedToUploadImage":
                    return "FailedToUploadImage";
                case "NoImage":
                    instructor.Image = null;
                    break;
            }
            try
            {
                await instructorRepository.AddAsync(instructor);
                return "InstructorAdded";
            }
            catch (Exception)
            {
                return "FailedToAddInstructor";
            }
        }
    }
}
