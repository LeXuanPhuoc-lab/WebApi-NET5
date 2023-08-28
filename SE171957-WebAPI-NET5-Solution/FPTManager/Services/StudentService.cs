using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class StudentService : IStudentService
    {
        private readonly PRN211DemoADOContext _context;

        public StudentService(PRN211DemoADOContext context)
        {
            _context = context;
        }

        public bool AddStudent(Student student)
        {
            _context.Add(student);
            return (_context.SaveChanges() > 0) ? true : false;
        }

        public Student GetById(int id)
        {
            return _context.Students // from students
                           .Where(x => x.StudentId == id)//where id
                           .FirstOrDefault();// select first student
        }
    }
}
