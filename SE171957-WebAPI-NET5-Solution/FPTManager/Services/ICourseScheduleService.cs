using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface ICourseScheduleService
    {
        bool AddCourseSchedule(int courseId, List<DateTime> teachingDates, int slot, int roomId);
        List<CourseScheduleResponse> GetByCourseId(int courseId);
        bool DeleteByCourseId(int courseId);
    }
}
