using FPTManager.Entities;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Repositories
{
    public interface IStudentRepository
    {
        Task<bool> CreateAsync(Student student);
        Task<Student> GetByIdAsync(int id);
        Task<List<Student>> GetAllSync();
    }
}
