using FluentValidation;
using FluentValidation.Results;
using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Payloads.Request;
using FPTManager.Services;
using FPTManager.Utils;
using FPTManager.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FPTManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;

        public AccountController(IOptionsMonitor<AppSettings> monitor,
            IAccountService accountService,
            IStudentService studentService)
        {
            _appSettings = monitor.CurrentValue;
            _accountService = accountService;
            _studentService = studentService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var isSucess =
                _accountService.Login(loginRequest.Username, loginRequest.Password);

            if(!isSucess)
            {
                return Unauthorized();
            }
            var account = _accountService.GetByUserName(loginRequest.Username);


            JwtHelper jwtHepler = new JwtHelper(_appSettings);
            //var token = jwtHepler.GenerateToken(account);
            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Data = null
            });
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            var studentModel = signUpRequest.ToStudentModel();

            var result = await _studentService.CreateAsync(studentModel);

            if (result.IsSuccess)
            {
                var students = await _studentService.GetAllAsync();
                studentModel = students[students.Count - 1];

                // Create Student Success -> Create new Account
                // get student just created
                var accountModel = signUpRequest.ToAccountModel(studentModel);
                var resultAsync = await _accountService.CreateAsync(accountModel);
            }
            return result.Match<IActionResult>(b =>
            {
                return Ok(new BaseResponse { StatusCode = 200, Message = "Create Success"});    
            }, exception =>
            {
                if (exception is ValidationException validationException)
                {
                    return BadRequest(validationException.ToProblemDetails());
                }

                return StatusCode(500);
            });
            //// get student just created
            //var students = await _studentService.GetAllAsync();
            //studentModel = students[students.Count];

            //// create account

            //if (createStudentSucess)
            //{
            //    var createAccSucess = _accountService.SignUp(new Account
            //    {
            //        Username = signUpRequest.UserName,
            //        Password = signUpRequest.Password,
            //        Email = signUpRequest.Email,
            //        Student = student
            //    });

            //    if (createAccSucess)
            //    {
            //        return Ok(new BaseResponse
            //        {
            //            StatusCode = StatusCodes.Status200OK,
            //            Message = "Create Account Sucess"
            //        });
            //    }
            //}
        }
    }
}
