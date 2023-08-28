using System;
using System.Collections.Generic;

#nullable disable

namespace FPTManager.Entities
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
        }

        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
