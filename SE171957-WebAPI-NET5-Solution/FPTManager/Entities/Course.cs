using System;
using System.Collections.Generic;

#nullable disable

namespace FPTManager.Entities
{
    public partial class Course
    {
        public Course()
        {
            CourseSchedules = new HashSet<CourseSchedule>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public int? SubjectId { get; set; }
        public int? InstructorId { get; set; }
        public int? TempId { get; set; }
        public int? CampusId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
