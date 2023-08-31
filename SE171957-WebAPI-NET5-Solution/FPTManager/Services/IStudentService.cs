using FPTManager.Models;
using FPTManager.Models.Response;
using LanguageExt.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IStudentService
    {
        Task<Result<bool>> CreateAsync(StudentModel student);

        Task<List<StudentModel>> GetAllAsync();
        public StudentResponse GetById(int id);
    }
}
