using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Entities;
using SchoolManagment.Data.Entities.Identity;

namespace SchoolManagment.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _encryptionProvider = new GenerateEncryptionProvider("6dfe220115a6495d6515a6");
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

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
            modelBuilder.UseEncryption(_encryptionProvider);
        }

    }
}
