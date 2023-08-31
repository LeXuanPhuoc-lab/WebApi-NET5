using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Models.Request;
using FPTManager.Models.Response;
using FPTManager.Services;
using FPTManager.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FPTManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        public static int[] slot = { 1, 2, 3, 4, 5, 6, 7, 8 };
        
        private readonly AppSettings _appSettings;
        private readonly ICourseService _courseService;
        private readonly ISubjectService _subjectService;
        private readonly IInstructorService _instructorService;
        private readonly IStudentService _studentService;
        private readonly ICourseScheduleService _courseScheduleService;
        private readonly IRollCallBookService _rollCallBookService;
        private readonly IStudentCourseService _studentCourseService;
        private readonly IRoomService _roomService;

        public CourseController(IOptionsMonitor<AppSettings> opMonitor,
            ICourseService courseService,
            ISubjectService subjectService,
            IInstructorService instructorService,
            IStudentService studentService,
            ICourseScheduleService courseScheduleService,
            IRollCallBookService rollCallBookService,
            IStudentCourseService studentCourseService,
            IRoomService roomService)
        {
            _appSettings = opMonitor.CurrentValue;
            _courseService = courseService;
            _subjectService = subjectService;
            _instructorService = instructorService;
            _studentService = studentService;
            _courseScheduleService = courseScheduleService;
            _rollCallBookService = rollCallBookService;
            _studentCourseService = studentCourseService;
            _roomService = roomService;
        }

        //[HttpGet]
        //public IActionResult GetAll(int page = 1)
        //{
        //    int pageSize = _appSettings.PageSize;
        //    var courses = _courseService.GetCourses();
            
        //    if (courses != null)
        //    {
        //        // Pagging
        //        var result = PaginatedList<CourseResponse>.CreateByList(courses, page, pageSize);

        //        return Ok(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status200OK,
        //            Data = new { 
        //                Items = result,
        //                PageIndex = result.PageIndex,
        //                TotalPage = result.TotalPage
        //            }
        //        });
        //    }
        //    else
        //    {
        //        return NotFound(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status404NotFound,
        //            Message = "Not Found Any Course"
        //        });
        //    }
        //}

        //[HttpGet("{Id}")]
        //public IActionResult GetById(int Id)
        //{
        //    var courses = _courseService.GetById(Id);
        //    if (courses != null)
        //    {
        //        return Ok(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status200OK,
        //            Data = courses
        //        });
        //    }
        //    else
        //    {
        //        return NotFound(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status404NotFound,
        //            Message = "Not Found Any Course"
        //        });
        //    }
        //}

        //[HttpGet("Subject/{Id}")]
        //public IActionResult GetBySubjectId(int Id)
        //{
        //    var courses = _courseService.GetBySubjectId(Id);

        //    if(courses != null)
        //    {
        //        // Page Size
        //        int pageSize = _appSettings.PageSize;
        //        //Pagging, default page 1
        //        var result = PaginatedList<CourseResponse>.CreateByList(courses, 1 ,pageSize);
        //        return Ok(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status200OK,
        //            Data = new { 
        //                Items = result,
        //                PageIndex = result.PageIndex,
        //                TotalPage = result.TotalPage
        //            }
        //        });
        //    }

        //    return NotFound(new BaseResponse { 
        //        StatusCode = StatusCodes.Status404NotFound,
        //        Message = "Not Found ANy Subjects"
        //    });
        //}

        //[HttpGet("Add")]
        //[Authorize]
        //public IActionResult AddCourse()
        //{
        //    var subjects = _subjectService.GetSubjects();
        //    var instructors = _instructorService.GetInstructors();

        //    // set subjects, instructors default
        //    subjects.Insert(0, new SubjectResponse { SubjectId = 0 });
        //    instructors.Insert(0, new InstructorResponse { InstructorId = 0 });

        //    return Ok(new BaseResponse { 
        //        StatusCode = StatusCodes.Status200OK,
        //        Data = new {
        //            Subjects = subjects,
        //            Instructors = instructors
        //        }
        //    });
        //}

        //[HttpPost("DoAdd")]
        //[Authorize]
        //public IActionResult DoAddCourse(CourseRequest req)
        //{
        //    var courseEntity = new Course
        //    {
        //        CourseCode = req.CourseCode,
        //        CourseDescription = req.CourseDescription,
        //        SubjectId = (req.SubjectId > 0) ? req.SubjectId : null,
        //        InstructorId = (req.InstructorId > 0) ? req.InstructorId : null,
        //    };


        //    var isSucess = _courseService.AddCourse(courseEntity);
        //    if (isSucess)
        //    {
        //        return Ok(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status200OK,
        //            Message = "Add Course Sucess!"
        //        });
        //    }

        //    return BadRequest();
        //}


        //[HttpGet("AddStudent")]
        //[Authorize]
        //public IActionResult AddStudent()
        //{
        //    var students = _studentService.GetStudents();
        //    var rooms = _roomService.GetRooms();

        //    return Ok(new BaseResponse { 
        //         StatusCode = StatusCodes.Status200OK,
        //         Data = new { 
        //            Students = students,
        //            Slot = slot,
        //            Rooms = rooms
        //         }
        //    });
        //}

        //[HttpPost("DoAddStudent")]
        //[Authorize]
        //public IActionResult DoAddStudent(int courseId, List<int> studentIds, int slot, int RoomId)
        //{
            
        //    // Check Course Exist
        //    var course = _courseService.GetById(courseId);

        //    if(course == null)
        //    {
        //        return NotFound();
        //    }

        //    // Add Student To Course
        //    foreach(int stuId in studentIds)
        //    {
        //        _studentCourseService.AddStudentCourse(new StudentCourse
        //        {
        //            CourseId = courseId,
        //            StudentId = stuId
        //        });
        //    }

        //    var courseSchedules = _courseScheduleService.GetByCourseId(courseId);
        //    if (courseSchedules == null)
        //    {
        //        // Generate Teaching Date from curr month to specific month
        //        int totalMonth = 1; // default 1 course has 3 months 
        //        List<DateTime> datetimes = DateTimeHelper.GenerateRangeFromToDateTime(totalMonth);
        //        // Add datetime list to teachingdate
        //        bool isSucess = _courseScheduleService.AddCourseSchedule(courseId, datetimes, slot, RoomId);
        //        // Get CourseSchedule by Id Again
        //        courseSchedules = _courseScheduleService.GetByCourseId(courseId);
        //    }

        //    foreach (CourseScheduleResponse cs in courseSchedules)
        //    {
        //        foreach (int stuId in studentIds)
        //        {
        //            // Check Exist Student in Roll Call book
        //            var findRCBook = _rollCallBookService.GetStudentSchedule(stuId, cs.TeachingScheduleId);
        //            if (findRCBook == null)// Not Found
        //            {
        //                var rollCallBook = new RollCallBook
        //                {
        //                    TeachingScheduleId = cs.TeachingScheduleId,
        //                    StudentId = stuId,
        //                    IsAbsence = true
        //                };
        //                // Add Student to Roll Call Book    
        //                _rollCallBookService.AddRollCallBook(rollCallBook);
        //            }
        //            else// Already Exist
        //            {
        //                var s = _studentService.GetById(stuId);
        //                return BadRequest(new BaseResponse
        //                {
        //                    StatusCode = StatusCodes.Status400BadRequest,
        //                    Message = $"Student {s.FirstName} {s.MidName} {s.LastName} is already in this class"
        //                });
        //            }
        //        }
        //    }

        //    // Generate Student RollBackCourse Schedule
        //    return Ok(new BaseResponse { 
        //        StatusCode = StatusCodes.Status200OK,
        //        Message = "Add Student Sucess"
        //    });
        //}

        //[HttpGet("Update/{Id}")]
        //[Authorize]
        //public IActionResult UpdateCourse(int Id)
        //{
        //    var subjects = _subjectService.GetSubjects();
        //    var instructors = _instructorService.GetInstructors();
        //    var course = _courseService.GetById(Id);

        //    return Ok(new BaseResponse { 
        //        StatusCode = StatusCodes.Status200OK,
        //        Data = new { 
        //            Subjects = subjects,
        //            Instructors = instructors,
        //            Course = course
        //        }
        //    });
        //}

        //[HttpPut("DoUpdate")]
        //[Authorize]
        //public IActionResult DoUpdateCourse(int courseId, CourseRequest req) 
        //{
        //    var course = _courseService.GetById(courseId);

        //    if(course == null) // check existed course
        //    {
        //        return NotFound(new BaseResponse
        //        {
        //            StatusCode = StatusCodes.Status404NotFound,
        //            Message = $"Not Found Course Id = {courseId}"
        //        });
        //    }

        //    // Do Update Course
        //    bool isSucess = _courseService.UpdateCourse(new Course{
        //        CourseId = courseId,
        //        CourseCode = req.CourseCode,
        //        CourseDescription = req.CourseDescription,
        //        InstructorId = req.InstructorId,
        //        SubjectId = req.SubjectId,
        //        TempId = req.TempId,
        //        CampusId = req.CampusId,
        //    });

        //    if (isSucess)
        //    {
        //        return Ok(new BaseResponse { 
        //            StatusCode = StatusCodes.Status200OK,
        //            Message = "Update Sucess"
        //        });
        //    }

        //    return BadRequest();
        //}

        //[HttpDelete("{Id}")]
        //public IActionResult DoDelete(int Id)
        //{
        //    // Get Course 
        //    var course = _courseService.GetById(Id);
        //    if(course == null)
        //    {
        //        return NotFound();
        //    }

        //    // Delete Course in StudentCourse
        //    _studentCourseService.DeleteByCourseId(Id);
        //    // Get All CourseSchedules By CourseId
        //    var courseSchedules = _courseScheduleService.GetByCourseId(Id);
        //    // Delete RollCallBook contain CourseSchedules
        //    if(courseSchedules != null)
        //    {
        //        foreach (CourseScheduleResponse csr in courseSchedules)
        //        {
        //            // Delete Schedule in RollCallBook
        //            _rollCallBookService.DeleteByTeachingScheduleId(csr.TeachingScheduleId);
        //        }
        //    }
        //    // Delete Schedule
        //    _courseScheduleService.DeleteByCourseId(Id);
        //    // Delete Course
        //    _courseService.DeleteById(Id);

        //    return Ok(new BaseResponse { 
        //        StatusCode = StatusCodes.Status200OK,
        //        Message = $"Delete Course {course.CourseCode} Sucess"
        //    });
        //}
    }
}
