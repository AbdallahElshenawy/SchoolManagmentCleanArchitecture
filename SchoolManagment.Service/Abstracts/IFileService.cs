using Microsoft.AspNetCore.Http;

namespace SchoolManagment.Service.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadImage(string location, IFormFile file);
    }
}
