using AutoMapper;
using FluentValidation;
using FPTManager.Models;
using FPTManager.Models.Response;
using FPTManager.Repositories;
using FPTManager.Validation;
using LanguageExt.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class StudentService : IStudentService
    {
        //private readonly PRN211DemoADOContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentModel> _validator;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository,
            IMapper mapper, IValidator<StudentModel> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateAsync(StudentModel student)
        {
            //var validationResult = await _validator.ValidateAsync(student);
            //if (!validationResult.IsValid)
            //{
            //    var validationException = new ValidationException(validationResult.Errors);
            //    return new Result<bool>(validationException);
            //}
            var studentEntity = student.ToStudentEntity(_mapper);
            return await _studentRepository.CreateAsync(studentEntity);
        }

        public StudentResponse GetById(int id)
        {
            //var student = _context.Students // from students
            //               .Where(x => x.StudentId == id)//where id
            //               .FirstOrDefault();// select first student

            //return _mapper.Map<StudentResponse>(student);
            return null;
        }

        public async Task<List<StudentModel>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllSync();

            return _mapper.Map<List<StudentModel>>(students);
        }


        public async Task<StudentModel> GetByIdAsync(int id)
        {
            var studentEntity = _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentModel>(studentEntity);
        }
    }
}
