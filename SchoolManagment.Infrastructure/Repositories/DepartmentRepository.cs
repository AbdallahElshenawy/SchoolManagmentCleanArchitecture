using SchoolManagment.Data.Entities;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Infrastructure.InfrastructureBases;

namespace SchoolManagment.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext context) : GenericRepository<Department>(context), IDepartmentRepository
    {

    }
}
