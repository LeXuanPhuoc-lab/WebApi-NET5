using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IRollCallBookService
    {
        bool AddRollCallBook(RollCallBook rollCallBook);
        RollCallBookResponse GetStudentSchedule(int studentId, int teachingScheduleId);
        bool DeleteByTeachingScheduleId(int teachingScheduleId);
    }
}
