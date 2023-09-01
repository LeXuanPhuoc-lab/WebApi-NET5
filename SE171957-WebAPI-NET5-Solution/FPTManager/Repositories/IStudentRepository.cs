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
        Task<Result<bool>> CreateAsync(Student student);
        Task<List<Student>> GetAllSync();
    }
}
