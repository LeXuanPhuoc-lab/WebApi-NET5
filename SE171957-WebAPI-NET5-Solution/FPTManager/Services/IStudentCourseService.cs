using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IStudentCourseService
    {
        public bool AddStudentCourse(StudentCourse studentCourse);
        public bool AddRangeStudentCourse(List<StudentCourse> studentCourses);
        public bool DeleteByCourseId(int courseId);
    }
}
