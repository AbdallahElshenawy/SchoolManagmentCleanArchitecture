using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Entities;

namespace SchoolManagment.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentSubject>()
                .HasKey(ds => new { ds.DID, ds.SubID });
            modelBuilder.Entity<InstructorSubject>()
               .HasKey(iS => new { iS.InsId, iS.SubId });
            modelBuilder.Entity<StudentSubject>()
               .HasKey(ss => new { ss.SubID, ss.StudID });
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.SuperVisor)
                .WithMany(i => i.Instructors)
                .HasForeignKey(i => i.SuperVisorId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
