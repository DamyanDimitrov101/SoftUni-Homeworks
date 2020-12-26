using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        //        •	Student:
        //o StudentId
        //o Name(up to 100 characters, unicode)
        //o PhoneNumber(exactly 10 characters, not unicode, not required)
        //o RegisteredOn
        //o Birthday(not required)


        public Student()
        {
            this.CourseEnrollments = new HashSet<StudentCourse>();
            
            this.HomeworkSubmissions = new HashSet<Homework>();
        }

        public int StudentId { get; set; }

        public string Name { get; set; }

        [MaxLength(10), MinLength(10)]
        public string PhoneNumber { get; set; }
        
        public DateTime RegisteredOn { get; set; }
        
        public DateTime? Birthday { get; set; }

        public virtual ICollection<StudentCourse> CourseEnrollments { get; set; }

        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }
    }
}
