using SchoolManagment.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Department
{
    [Key]
    public int DID { get; set; }

    [StringLength(500)]
    public string DName { get; set; }

    public int? InsManagerId { get; set; } // FK to Instructor who manages this department

    [ForeignKey("InsManagerId")]
    [InverseProperty("DepartmentManager")]
    public virtual Instructor InsManager { get; set; }

    public virtual ICollection<Instructor> Instructors { get; set; }
    public virtual ICollection<Student> Students { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
}
