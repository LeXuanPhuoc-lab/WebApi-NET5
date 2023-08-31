using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly PRN211DemoADOContext _context;

        public InstructorService(PRN211DemoADOContext context)
        {
            _context = context;
        }

        public List<InstructorResponse> GetInstructors()
        {
            var instructors = _context.Instructors.ToList();

            return instructors.Select(x => new InstructorResponse { 
                InstructorId = x.InstructorId,
                FirstName = x.FirstName,
                MidName = x.MidName, 
                LastName = x.LastName
            }).ToList();
        }
    }
}
