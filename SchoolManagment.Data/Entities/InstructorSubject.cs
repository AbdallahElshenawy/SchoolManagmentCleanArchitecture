using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagment.Data.Entities
{
    public class InstructorSubject
    {
        public int InsId { get; set; }
        public int SubId { get; set; }

        [InverseProperty(nameof(Instructor.InstructorsSubjects))]
        public Instructor? Instructor { get; set; }

        [InverseProperty("InstructorSubjects")]
        public Subject? Subject { get; set; }

    }
}
