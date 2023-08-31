using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly PRN211DemoADOContext _context;

        public StudentCourseService(PRN211DemoADOContext context)
        {
            _context = context;
        }

        public bool AddRangeStudentCourse(List<StudentCourse> studentCourses)
        {
            _context.AddRange(studentCourses);
            return (_context.SaveChanges() > 0) ? true : false;
        }

        public bool AddStudentCourse(StudentCourse studentCourse)
        {
            _context.StudentCourses.Add(studentCourse);
            return (_context.SaveChanges() > 0) ? true : false;
        }

        public bool DeleteByCourseId(int courseId)
        {
            var stuCourses = _context.StudentCourses.Where(x => x.CourseId == courseId).ToList();
            _context.StudentCourses.RemoveRange(stuCourses);
            return (_context.SaveChanges() > 0) ? true : false;
        }
    }
}
