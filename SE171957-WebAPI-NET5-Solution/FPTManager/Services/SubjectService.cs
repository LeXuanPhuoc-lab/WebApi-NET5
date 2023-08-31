using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly PRN211DemoADOContext _context;

        public SubjectService(PRN211DemoADOContext context)
        {
            _context = context;
        }

        public List<SubjectResponse> GetSubjects()
        {
            var subjects = _context.Subjects.ToList();

            return subjects.Select(x => new SubjectResponse { 
                SubjectId = x.SubjectId,
                SubjectName = x.SubjectName
            }).ToList();
        }
    }
}
