namespace SchoolManagment.Core.Features.Students.Queries.Results
{
    public class GetDepartmentResponse
    {
        public int Id { get; set; }
        public string DName { get; set; }

        public string DepartmentManager { get; set; }
        public List<StudentResponse>? Students { get; set; }
        public List<SubjectResponse>? Subjects { get; set; }
        public List<InstructorResponse>? Instructors { get; set; }


    }
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}