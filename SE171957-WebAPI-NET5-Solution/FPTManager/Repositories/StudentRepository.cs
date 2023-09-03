using FPTManager.Entities;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PRN211DemoADOContext _context;

        public StudentRepository(PRN211DemoADOContext context){ _context = context; }
        public async Task<bool> CreateAsync(Student student)
        {
            await _context.AddAsync(student);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Student>> GetAllSync()
        {
            var students =  _context.Students.ToList();
            return students;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return _context.Students
                           .Where(x => x.StudentId == id)
                           .FirstOrDefault();
        }
    }
}
