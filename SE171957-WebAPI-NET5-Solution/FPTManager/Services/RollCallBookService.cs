using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class RollCallBookService : IRollCallBookService
    {
        private readonly PRN211DemoADOContext _context;
        private readonly IMapper _mapper;

        public RollCallBookService(PRN211DemoADOContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool AddRollCallBook(RollCallBook rollCallBook)
        {
            _context.RollCallBooks.Add(rollCallBook);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteByTeachingScheduleId(int teachingScheduleId)
        {
            var rcBooks = _context.RollCallBooks
                                 .Where(x => x.TeachingScheduleId == teachingScheduleId)
                                 .ToList();
            if(rcBooks != null)
            {
                _context.RollCallBooks.RemoveRange(rcBooks);
            }
            return _context.SaveChanges() > 0 ? true : false;
        }

        public RollCallBookResponse GetStudentSchedule(int studentId, int teachingScheduleId)
        {
            var rcBook = _context.RollCallBooks
                                .Where(x => x.StudentId == studentId && x.TeachingScheduleId == teachingScheduleId)
                                .FirstOrDefault();

            if(rcBook != null)
            {
                return _mapper.Map<RollCallBookResponse>(rcBook);
            }
            return null;
        }
    }
}
