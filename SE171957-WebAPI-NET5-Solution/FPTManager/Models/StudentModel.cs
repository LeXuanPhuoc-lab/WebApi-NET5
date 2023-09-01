using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string Roll { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public virtual AccountResponse Account { get; set; }
        //public virtual ICollection<RollCallBook> RollCallBooks { get; set; }
        //public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }

    public static class StudentModelExtension
    {
        public static Student ToStudentEntity(this StudentModel studentModel, IMapper mapper)
        {
            return mapper.Map<Student>(studentModel);
        }
    }
}
