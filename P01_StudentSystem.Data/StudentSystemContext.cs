using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                //•	Course:
                //o CourseId
                //o Name(up to 80 characters, unicode)
                //o Description(unicode, not required)
                //o StartDate
                //o EndDate
                //o Price

                entity.HasKey(c=> c.CourseId);

                entity.Property(c => c.Name)
                .IsUnicode()
                .HasMaxLength(80);

                entity.Property(c => c.Description)
                .IsUnicode()
                .IsRequired(false);

             
            });

            modelBuilder.Entity<Student>(entity =>
            {
                //        •	Student:
                //o StudentId
                //o Name(up to 100 characters, unicode)
                //o PhoneNumber(exactly 10 characters, not unicode, not required)
                //o RegisteredOn
                //o Birthday(not required)

                entity.HasKey(s => s.StudentId);

                entity.Property(s => s.Name)
                .IsUnicode()
                .HasMaxLength(100);

                entity.Property(s => s.PhoneNumber)
                .IsUnicode(false)
                .IsRequired(false);

                entity.Property(s => s.Birthday)
                .IsRequired(false);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                //•	Resource:
                //o ResourceId
                //o Name(up to 50 characters, unicode)
                //o Url(not unicode)
                //o ResourceType(enum – can be Video, Presentation, Document or Other)
                //o CourseId

                entity.HasKey(r => r.ResourceId);

                entity.Property(r => r.Name)
                .HasMaxLength(50)
                .IsUnicode();

                entity.Property(r => r.Url)
                .IsUnicode(false);

                entity
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                //•	Homework:
                //o HomeworkId
                //o Content(string, linking to a file, not unicode)
                //o ContentType(enum – can be Application, Pdf or Zip)
                //o SubmissionTime
                //o StudentId
                //o CourseId

                entity.HasKey(h => h.HomeworkId);

                entity.Property(h => h.Content).IsUnicode(false);

                entity
                .HasOne(h => h.Course)
                .WithMany(c => c.HomeworkSubmissions)
                .HasForeignKey(h=>h.CourseId);

                entity
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(h => h.StudentId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc=> new { sc.StudentId, sc.CourseId });

                entity
                .HasOne(sc => sc.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(sc => sc.StudentId);

                entity
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(sc => sc.CourseId);
            });
        }
    }
}
