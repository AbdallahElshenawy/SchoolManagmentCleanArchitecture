using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagment.Data.Entities;
using SchoolManagment.Data.Enums;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class StudentService(IStudentRepository studentRepository, ILogger<StudentService> logger) : IStudentService
    {
        public async Task<string> AddStudentAsync(Student student)
        {
            var studentDb = await studentRepository.GetTableAsTracking()
                 .Where(s => s.Name == student.Name).FirstOrDefaultAsync();
            if (studentDb != null)
            {
                logger.LogWarning("AddStudentAsync: Student already exists with name {StudentName}", student.Name);
                return "Student already exists";
            }

            await studentRepository.AddAsync(student);
            return "Student added successfully";
        }

        public async Task<string> DeleteStudentAsync(Student student)
        {
            logger.LogInformation("Deleting student with ID: {StudentId}", student.StudID);
            await studentRepository.DeleteAsync(student);
            return "Success";
        }

        public async Task<string> EditStudentAsync(Student student)
        {
            logger.LogInformation("Editing student with ID: {StudentId}", student.StudID);
            await studentRepository.UpdateAsync(student);
            return "Success";
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum order)
        {
            logger.LogInformation("Filtering students with search: {Search} and order: {Order}", search, order);
            var query = studentRepository.GetTableNoTracking()
                .Include(s => s.Department)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(s =>
                    s.Name.ToLower().Contains(search.ToLower()) ||
                    s.Address.ToLower().Contains(search.ToLower()) ||
                    (s.Department != null && s.Department.DName.ToLower().Contains(search.ToLower())));
            switch (order)
            {
                case StudentOrderingEnum.Name:
                    query = query.OrderBy(s => s.Name);
                    break;
                case StudentOrderingEnum.Address:
                    query = query.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    query = query.OrderBy(s => s.Department.DName);
                    break;
                default:
                    query = query.OrderBy(s => s.StudID);
                    break;
            }
            return query;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await studentRepository.GetTableNoTracking()
                    .Include(s => s.Department)
                    .Where(s => s.StudID == id)
                    .FirstOrDefaultAsync();
        }

        public IQueryable<Student> GetStudentsAsQuerable()
        {
            logger.LogInformation("Fetching all students as queryable with department info.");

            return studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await studentRepository.GetAllStudentsAsync();
        }
    }
}
