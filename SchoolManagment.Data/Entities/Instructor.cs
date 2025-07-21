using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagment.Data.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InstructorsSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        public int InsId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Postion { get; set; }
        public int? SuperVisorId { get; set; }
        public decimal Salary { get; set; }
        public int DID { get; set; }
        public Department Department { get; set; }
        [InverseProperty("InsManager")]
        public Department DepartmentManager { get; set; }

        [ForeignKey("SuperVisorId")]
        [InverseProperty("Instructors")]
        public Instructor SuperVisor { get; set; }

        [InverseProperty("SuperVisor")]

        public ICollection<Instructor> Instructors { get; set; }
        [InverseProperty(nameof(InstructorSubject.Instructor))]
        public ICollection<InstructorSubject> InstructorsSubjects { get; set; }

    }
}
