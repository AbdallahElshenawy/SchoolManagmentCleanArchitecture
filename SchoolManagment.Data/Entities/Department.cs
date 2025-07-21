using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagment.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }
        [Key]
        public int DID { get; set; }
        [StringLength(500)]
        public string DName { get; set; }
        public int? InsManagerId { get; set; }
        [ForeignKey("InsManagerId")]
        [InverseProperty("DepartmentManager")]
        public virtual Instructor InsManager { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }

}
