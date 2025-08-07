using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class InstructorService(IFileService fileService, IInstructorRepository instructorRepository, ILogger<InstructorService> logger) : IInstructorService
    {
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {

            var imageUrl = await fileService.UploadImage("instructors", file);
            instructor.Image = imageUrl;
            switch (imageUrl)
            {
                case "FailedToUploadImage":
                    logger.LogWarning("Image upload failed for instructor: {Name}", instructor?.Name);
                    return "FailedToUploadImage";
                case "NoImage":
                    logger.LogInformation("No image was provided for instructor: {Name}", instructor?.Name);
                    instructor.Image = null;
                    break;
            }
            try
            {
                await instructorRepository.AddAsync(instructor);
                return "InstructorAdded";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add instructor: {Name}", instructor?.Name);

                return "FailedToAddInstructor";
            }
        }
    }
}
