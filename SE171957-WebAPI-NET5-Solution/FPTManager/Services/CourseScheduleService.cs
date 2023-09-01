using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class CourseScheduleService : ICourseScheduleService
    {
        private readonly PRN211DemoADOContext _context;

        public CourseScheduleService(PRN211DemoADOContext context)
        {
            _context = context;
        }
        public bool AddCourseSchedule(int courseId, List<DateTime> teachingDates, int slot, int roomId)
        {
            List<CourseSchedule> list = new List<CourseSchedule>();
            foreach(DateTime dt in teachingDates)
            {
                CourseSchedule cs = new CourseSchedule
                {
                    CourseId = courseId,
                    TeachingDate = dt,
                    Slot = slot,
                    RoomId = roomId
                };
                list.Add(cs);
            }

            _context.CourseSchedules.AddRange(list);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteByCourseId(int courseId)
        {
            var courseSchedules = _context.CourseSchedules
                                          .Where(x => x.CourseId == courseId)
                                          .ToList();
            if(courseSchedules != null)
            {
                _context.CourseSchedules.RemoveRange(courseSchedules);
            }
            return _context.SaveChanges() > 0 ? true : false;
        }

        public List<CourseScheduleResponse> GetByCourseId(int courseId)
        {
            var courseSchedules = 
                _context.CourseSchedules.Where(x => x.CourseId == courseId).ToList();
            if(courseSchedules.Count > 0)
            {
                return courseSchedules.Select(cs => new CourseScheduleResponse
                {
                    CourseId = cs.CourseId,
                    TeachingScheduleId = cs.TeachingScheduleId,
                    TeachingDate = cs.TeachingDate,
                    RoomId = cs.RoomId,
                    Description = cs.Description,
                    Slot = cs.Slot
                }).ToList();
            }
            return null;
        }
    }
}
