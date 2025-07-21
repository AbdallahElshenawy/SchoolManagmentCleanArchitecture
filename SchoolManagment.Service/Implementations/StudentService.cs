using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Entities;
using SchoolManagment.Data.Helper;
using SchoolManagment.Infrastructure.Abstracts;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class StudentService(IStudentRepository studentRepository) : IStudentService
    {
        public async Task<string> AddStudentAsync(Student student)
        {
            var studentDb = await studentRepository.GetTableAsTracking()
                 .Where(s => s.Name == student.Name).FirstOrDefaultAsync();
            if (studentDb != null)
                return "Student already exists";

            await studentRepository.AddAsync(student);
            return "Student added successfully";
        }

        public async Task<string> DeleteStudentAsync(Student student)
        {
            await studentRepository.DeleteAsync(student);
            return "Success";
        }

        public async Task<string> EditStudentAsync(Student student)
        {
            await studentRepository.UpdateAsync(student);
            return "Success";
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum order)
        {
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
            return studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await studentRepository.GetAllStudentsAsync();
        }
    }
}
