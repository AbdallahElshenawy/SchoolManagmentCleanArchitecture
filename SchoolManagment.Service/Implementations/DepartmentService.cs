using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Entities;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await departmentRepository.GetTableNoTracking()
                 .Where(d => d.DID.Equals(id))
                 .Include(d => d.Students)
                 .Include(d => d.DepartmentSubjects).ThenInclude(d => d.Subject)
                 .Include(d => d.Instructors)
                 .Include(d => d.InsManager)
                 .FirstOrDefaultAsync();
        }
    }
}
