using EFMVCDemo2.Models;
using Microsoft.EntityFrameworkCore;

namespace EFMVCDemo2.Context
{
    public class MVCContext : DbContext
    {
        public MVCContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasOne(sub => sub.subject)
                .WithMany(ss => ss.StudentSubject)
                .HasForeignKey(subid => subid.SubjectId);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(stu => stu.student)
                .WithMany(ss => ss.StudentSubject)
                .HasForeignKey(stuid => stuid.StudentId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne(sub => sub.subject)
                .WithMany(ts => ts.TeacherSubject)
                .HasForeignKey(subid => subid.SubjectId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne(tea => tea.teacher)
                .WithMany(ts => ts.TeacherSubject)
                .HasForeignKey(teaid => teaid.TeacherId);
                
        }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentSubject> StudentSubjects { get; set; }

        public DbSet<EFMVCDemo2.Models.TeacherSubject>? TeacherSubject { get; set; }



    }
}
