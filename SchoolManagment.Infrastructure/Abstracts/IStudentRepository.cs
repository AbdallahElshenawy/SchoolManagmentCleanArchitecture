using SchoolManagment.Data.Entities;
using SchoolManagment.Infrastructure.InfrastructureBases;

namespace SchoolManagment.Infrastructure.Abstracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<List<Student>> GetAllStudentsAsync();
    }
}
