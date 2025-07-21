using AutoMapper;

namespace SchoolManagment.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentListMapping();
            GetStudentResponseMapping();
            AddStudentCommandMapping();
            EditStudentCommandMapping();
        }
    }
}
