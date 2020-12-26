using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        //•	Course:
        //o CourseId
        //o Name(up to 80 characters, unicode)
        //o Description(unicode, not required)
        //o StartDate
        //o EndDate
        //o Price

        public Course()
        {
            this.StudentsEnrolled = new HashSet<StudentCourse>();

            this.HomeworkSubmissions = new HashSet<Homework>();

            this.Resources = new HashSet<Resource>();
        }

        public int CourseId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<StudentCourse> StudentsEnrolled { get; set; }

        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
