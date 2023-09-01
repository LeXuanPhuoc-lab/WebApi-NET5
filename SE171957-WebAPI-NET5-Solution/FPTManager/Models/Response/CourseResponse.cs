using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class CourseResponse
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public int? TempId { get; set; }
        public int? CampusId { get; set; }

        public InstructorResponse Instructor { get; set; }
        public SubjectResponse Subject { get; set; }
        //public ICollection<CourseSchedule> CourseSchedules { get; set; }
        //public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
