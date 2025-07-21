using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagment.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmentsSubjects = new HashSet<DepartmentSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        public int SubID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public int Period { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<DepartmentSubject> DepartmentsSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<InstructorSubject> InstructorSubjects
        {
            get; set;
        }
    }
}
