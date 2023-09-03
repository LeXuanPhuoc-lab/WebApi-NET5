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
        Task<bool> CreateAsync(StudentModel student);
        Task<List<StudentModel>> GetAllAsync();
        Task<StudentModel> GetByIdAsync(int id);
        public StudentResponse GetById(int id);
    }
}
