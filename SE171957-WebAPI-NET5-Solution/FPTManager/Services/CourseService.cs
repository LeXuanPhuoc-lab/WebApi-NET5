using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models.Response;
using FPTManager.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class CourseService : ICourseService
    {
        private readonly PRN211DemoADOContext _context;
        private readonly IMapper _mapper;

        public CourseService(PRN211DemoADOContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddCourse(Course course) 
        {
            try
            {
                _context.Courses.Add(course);
                return (_context.SaveChanges() > 0) ? true : false;
            }
            catch(DbUpdateException e)
            {
                throw new DbUpdateException(nameof(course), e);
            }
        }

        public bool DeleteById(int Id)
        {
            var course 
                = _context.Courses.Where(x => x.CourseId == Id).FirstOrDefault();
            if(course != null)
            {
                _context.Courses.Remove(course);
            }
            return (_context.SaveChanges() > 0) ? true : false;
        }

        public CourseResponse GetById(int Id)
        {
            var course = _context.Courses.Where(x => x.CourseId == Id).FirstOrDefault();

            if(course != null)
            {
                var Subject = _context.Subjects // from Subjects
                                      .Where(x => x.SubjectId == course.SubjectId) // Where x.SubjectId == c.SubjectId
                                      .FirstOrDefault(); // select First
                var Instructor = _context.Instructors// from Courses
                                         .Where(x => x.InstructorId == course.InstructorId)// Where x.InstructorId == c.InstructorId
                                         .FirstOrDefault(); // select First
                // set to courseResponse 
                course.Subject = Subject;
                course.Instructor = Instructor;

                return _mapper.Map<CourseResponse>(course);
            }
            return null;
        }

        public List<CourseResponse> GetBySubjectId(int SubjectId)
        {
            var courses = _context.Courses.Where(x => x.SubjectId == SubjectId).ToList();

            if(courses != null)
            {
                foreach(Course c in courses)
                {
                    var Subject = _context.Subjects
                                          .Where(x => x.SubjectId == c.SubjectId)
                                          .FirstOrDefault();
                    var Instructor = _context.Instructors
                                             .Where(x => x.InstructorId == c.InstructorId)
                                             .FirstOrDefault();

                    c.Subject = Subject;
                    c.Instructor = Instructor;
                }

                return courses.Select(x => new CourseResponse { 
                    CourseId = x.CourseId,
                    CourseCode = x.CourseCode,
                    CourseDescription = x.CourseDescription,
                    Instructor = _mapper.Map<InstructorResponse>(x.Instructor),
                    Subject = _mapper.Map<SubjectResponse>(x.Subject)
                }).ToList();
            }
            return null;
        }

        public List<CourseResponse> GetCourses()
        {
            var courses = _context.Courses.ToList();

            foreach (Course c in courses)
            {
                var Subject = _context.Subjects // from Subjects
                                      .Where(x => x.SubjectId == c.SubjectId) // Where x.SubjectId == c.SubjectId
                                      .FirstOrDefault(); // select First
                var Instructor = _context.Instructors// from Courses
                                         .Where(x => x.InstructorId == c.InstructorId)// Where x.InstructorId == c.InstructorId
                                         .FirstOrDefault(); // select First
                // set to courseResponse 
                c.Subject = Subject;
                c.Instructor = Instructor;
            }


            return _mapper.Map<List<CourseResponse>>(courses);
        }

        public bool UpdateCourse(Course newCourse)
        {
            var course = _context.Courses
                                 .Where(x => x.CourseId == newCourse.CourseId)
                                 .FirstOrDefault();
            if (course != null)
            {
                course.CourseCode = newCourse.CourseCode;
                course.CourseDescription = newCourse.CourseDescription;
                course.InstructorId = newCourse.InstructorId;
                course.SubjectId = newCourse.SubjectId;
                course.TempId = newCourse.TempId;
                course.CampusId = newCourse.CampusId;
            }

            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
