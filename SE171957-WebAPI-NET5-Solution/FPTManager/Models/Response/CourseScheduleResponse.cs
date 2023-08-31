using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class CourseScheduleResponse
    {
        public int TeachingScheduleId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? TeachingDate { get; set; }
        public int? Slot { get; set; }
        public int? RoomId { get; set; }
        public string Description { get; set; }

        public virtual CourseResponse Course { get; set; }
        public virtual RoomResponse Room { get; set; }
    }
}
