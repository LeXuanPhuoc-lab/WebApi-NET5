using System;
using System.Collections.Generic;

#nullable disable

namespace FPTManager.Entities
{
    public partial class RollCallBook
    {
        public int RollCallBookId { get; set; }
        public int? TeachingScheduleId { get; set; }
        public int? StudentId { get; set; }
        public bool? IsAbsence { get; set; }
        public string Comment { get; set; }

        public virtual Student Student { get; set; }
        public virtual CourseSchedule TeachingSchedule { get; set; }
    }
}
