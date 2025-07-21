using SchoolManagment.Data.Entities;
using SchoolManagment.Data.Helper;

namespace SchoolManagment.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync();
        IQueryable<Student> GetStudentsAsQuerable();
        IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum order);
        Task<Student> GetStudentByIdAsync(int id);
        Task<string> AddStudentAsync(Student student);
        Task<string> EditStudentAsync(Student student);
        Task<string> DeleteStudentAsync(Student student);
    }
}
