using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class RollCallBookResponse
    {
        public int RollCallBookId { get; set; }
        public int? TeachingScheduleId { get; set; }
        public int? StudentId { get; set; }
        public bool? IsAbsence { get; set; }
        public string Comment { get; set; }

        public virtual StudentResponse Student { get; set; }
        public virtual CourseScheduleResponse TeachingSchedule { get; set; }
    }
}
