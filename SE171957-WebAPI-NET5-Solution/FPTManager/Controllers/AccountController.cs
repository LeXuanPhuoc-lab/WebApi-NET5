using FPTManager.Models;
using FPTManager.Payloads.Request;
using FPTManager.Services;
using FPTManager.Utils;
using FPTManager.Validation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FPTManager.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "StudentsCacheKey";

        public AccountController(IOptionsMonitor<AppSettings> monitor,
            IAccountService accountService,
            IStudentService studentService,
            IMemoryCache cache)
        {
            _appSettings = monitor.CurrentValue;
            _accountService = accountService;
            _studentService = studentService;
            _cache = cache;
        }

        [HttpPost("account/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var isSucess = await _accountService.Login(loginRequest.Username, loginRequest.Password);

            if (!isSucess)
            {
                return Unauthorized();
            }
            var account = await _accountService.GetByUserNameAsync(loginRequest.Username);

            JwtHelper jwtHepler = new JwtHelper(_appSettings);
            var token = jwtHepler.GenerateToken(account);
            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Data = token
            });
        }

        [HttpPost("account/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            var validator = new SignUpValidator();
            var result = await validator.ValidateAsync(signUpRequest);
            if (!result.IsValid)
            {
                return BadRequest(result.ToProblemDetails());
            }

            // check exist username and email
            var email = await _accountService.GetByEmailAsync(signUpRequest.Email);
            if (email != null)
                return BadRequest(new BaseResponse { StatusCode = 400, Message = "Email already exist" });


            var studentModel = signUpRequest.ToStudentModel();
            await _studentService.CreateAsync(studentModel);

            var students = await _studentService.GetAllAsync();
            studentModel = students[students.Count - 1];

            // Create Student Success -> Create new Account
            // get student just created
            var accountModel = signUpRequest.ToAccountModel(studentModel);
            await _accountService.CreateAsync(accountModel);

            return Ok(new BaseResponse { StatusCode = 200, Message = "Create Success!" });
        }

        [HttpGet("account/cacheDemo")]
        public async Task<IActionResult> GetAll()
        {
            this.ClearCache<StudentModel>(cacheKey, _cache);

            if(!_cache.TryGetValue(cacheKey, out IEnumerable<StudentModel> students))
            {
                students = await _studentService.GetAllAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cacheKey, students, cacheEntryOptions);
            }

            return Ok(students);
        }

        [HttpPost("account/uploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file) 
        {

            if(file is null) 
            {
                return BadRequest(new BaseResponse { StatusCode = 400, Message = "Upload File Fail" });
            }

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "Upload\\Images\\");

            // init if not found
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();            }
            // combine current path with filename
            var fileExtension = file.FileName.Split(".")[1];
            var guidId = Guid.NewGuid();
            path += guidId + "." + fileExtension;
            await file.CopyToAsync(new FileStream(path, FileMode.Create));
            return Ok(new BaseResponse { StatusCode = 200, Message = "Upload File Success", Data = guidId });
        }

        [HttpPost("account/{id:Guid}/image")]
        public async Task<IActionResult> DownloadFile([FromRoute] Guid id)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Images\\");

            // Get file extension
            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles();
            foreach(var f in files)
            {
                if (f.FullName.Contains(id.ToString()))
                {
                    path += id + f.Extension;
                }
            }


            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out string contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            //using (var sr = new StreamReader(path))
            //{
            //    String AsString = sr.ReadToEnd();
            //    byte[] AsBytes = new byte[AsString.Length];
            //    Buffer.BlockCopy(AsString.ToCharArray(), 0, AsBytes, 0, AsBytes.Length);
            //    String AsBase64String = Convert.ToBase64String(AsBytes);

            //    var tempBytes = Convert.FromBase64String(AsBase64String);
            //    return File(tempBytes, contenttype, Path.GetFileName(path));
            //}

            return File(bytes, contenttype, Path.GetFileName(path));
        }

        [HttpGet("account/demo")]
        public string Get() {
            return "HEllo";
        }
    }
}
