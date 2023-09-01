using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Mapping
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {
            CreateMap<Course, CourseResponse>().ReverseMap();
            //CreateMap<Student, StudentResponse>().ReverseMap();
            CreateMap<Room, RoomResponse>().ReverseMap();
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Account, AccountModel>().ReverseMap();
        }
    }
}
