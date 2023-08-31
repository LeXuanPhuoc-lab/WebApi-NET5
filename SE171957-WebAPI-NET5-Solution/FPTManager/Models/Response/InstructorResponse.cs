using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class InstructorResponse
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }

        //public virtual ICollection<CourseResponse> Courses { get; set; }
    }
}
