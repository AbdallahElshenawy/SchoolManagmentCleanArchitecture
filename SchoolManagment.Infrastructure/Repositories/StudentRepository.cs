using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Entities;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Infrastructure.InfrastructureBases;

namespace SchoolManagment.Infrastructure.Repositories
{
    public class StudentRepository(AppDbContext dbContext) : GenericRepository<Student>(dbContext), IStudentRepository
    {
          public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await dbContext.Students.Include(s=>s.Department).ToListAsync()!; 
        }
    }
}
