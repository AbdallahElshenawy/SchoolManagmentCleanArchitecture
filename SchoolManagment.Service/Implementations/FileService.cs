using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class FileService(IHostEnvironment hostEnvironment) : IFileService
    {
        public async Task<string> UploadImage(string location, IFormFile file)
        {
            var webRootPath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot");
            var path = Path.Combine(webRootPath, location);

            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;

            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fullPath = Path.Combine(path, fileName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return $"/{location}/{fileName}";
                    }
                }
                catch (Exception)
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }
        }
    }
}