using FPTManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IStudentService
    {
        public bool AddStudent(Student student);
        public Student GetById(int id);
    }
}
