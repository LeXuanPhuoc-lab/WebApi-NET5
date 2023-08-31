using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface ICourseService
    {
        List<CourseResponse> GetCourses();
        CourseResponse GetById(int Id);
        List<CourseResponse> GetBySubjectId(int SubjectId);
        bool AddCourse(Course course);
        bool UpdateCourse(Course newCourse);
        bool DeleteById(int Id);
    }
}
