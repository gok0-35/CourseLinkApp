using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using courseLinkApp.Data;

namespace courseLinkApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Required]
        [StringLength(50)]
        public string? CourseName { get; set; }
        public int TeacherId { get; set; }
        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();

    }
}