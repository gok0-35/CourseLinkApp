using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courseLinkApp.Data
{
    public class CourseRegistration
    {
        [Key]
        public int CourseRegistrationId { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
    }
}