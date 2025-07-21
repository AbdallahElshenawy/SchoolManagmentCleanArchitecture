using SchoolManagment.Data.Entities;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Infrastructure.InfrastructureBases;

namespace SchoolManagment.Infrastructure.Repositories
{
    public class InstructorRepository(AppDbContext context) : GenericRepository<Instructor>(context), IInstructorRepository
    {
    }
}
