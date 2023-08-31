using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models.Request
{
    public class CourseRequest
    {
        public int CourseId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public int? SubjectId { get; set; }
        public int? InstructorId { get; set; }
        public int? TempId { get; set; }
        public int? CampusId { get; set; }
    }
}
