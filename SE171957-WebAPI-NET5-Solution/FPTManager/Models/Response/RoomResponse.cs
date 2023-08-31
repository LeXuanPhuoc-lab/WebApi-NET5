using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Response
{
    public class RoomResponse
    {
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public int? CampusId { get; set; }
        public string Cap2acity { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
