using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class SubjectResponse
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        //public virtual ICollection<CourseResponse> Courses { get; set; }
    }
}
