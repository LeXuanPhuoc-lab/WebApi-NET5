using FPTManager.Models;
using FPTManager.Models.Response;
using FPTManager.Services;
using FPTManager.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ISubjectService _subjectService;

        public SubjectController(IOptionsMonitor<AppSettings> monitor, ISubjectService subjectService)
        {
            _appSettings = monitor.CurrentValue;
            _subjectService = subjectService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll(int page)
        {
            var subjects = _subjectService.GetSubjects();

            if(subjects != null)
            {
                return Ok(new BaseResponse { 
                    StatusCode = StatusCodes.Status200OK,
                    Data = subjects
                });
            }
            else
            {
                return NotFound(new BaseResponse {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Not Found Any Subjects"
                });
            }
        }



    }
}
