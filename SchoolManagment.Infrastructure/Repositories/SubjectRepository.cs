using SchoolManagment.Data.Entities;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Infrastructure.InfrastructureBases;

namespace SchoolManagment.Infrastructure.Repositories
{
    public class SubjectRepository(AppDbContext context) : GenericRepository<Subject>(context), ISubjectRepository
    {
    }
}
