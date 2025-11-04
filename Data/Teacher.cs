using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace courseLinkApp.Data
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string NameFull
        {
            get { return this.Name + " " + this.LastName; }
        }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}