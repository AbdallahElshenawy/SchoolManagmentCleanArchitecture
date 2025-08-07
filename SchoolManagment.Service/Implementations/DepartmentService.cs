using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class DepartmentService(IDepartmentRepository departmentRepository, ILogger<DepartmentService> logger) : IDepartmentService
    {
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            try
            {
                logger.LogInformation("Fetching department with ID {DepartmentId}", id);

                var department = await departmentRepository.GetTableNoTracking()
                    .Where(d => d.DID == id)
                    .Include(d => d.Students)
                    .Include(d => d.DepartmentSubjects).ThenInclude(d => d.Subject)
                    .Include(d => d.Instructors)
                    .Include(d => d.InsManager)
                    .FirstOrDefaultAsync();

                if (department == null)
                {
                    logger.LogWarning("Department with ID {DepartmentId} not found", id);
                }
                else
                {
                    logger.LogInformation("Successfully retrieved department with ID {DepartmentId}", id);
                }

                return department;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving department with ID {DepartmentId}", id);
                throw;
            }
        }
    }
}
