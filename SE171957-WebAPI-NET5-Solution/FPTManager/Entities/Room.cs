using System;
using System.Collections.Generic;

#nullable disable

namespace FPTManager.Entities
{
    public partial class Room
    {
        public Room()
        {
            CourseSchedules = new HashSet<CourseSchedule>();
        }

        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public int? CampusId { get; set; }
        public string Cap2acity { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
    }
}
