using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courseLinkApp.Data
{
    public class Course
    {
        public int CourseId { get; set; }

        public string? CourseName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
    }
}