using Microsoft.AspNetCore.Http;

namespace SchoolManagment.Service.Abstracts
{
    public interface IInstructorService
    {
        Task<string> AddInstructorAsync(Instructor instructor, IFormFile file);
    }
}
