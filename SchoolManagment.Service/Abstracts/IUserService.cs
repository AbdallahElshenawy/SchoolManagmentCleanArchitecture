using SchoolManagment.Data.Entities.Identity;

namespace SchoolManagment.Service.Abstracts
{
    public interface IUserService
    {
        Task<string> AddUserAsync(User user, string password);
    }
}
