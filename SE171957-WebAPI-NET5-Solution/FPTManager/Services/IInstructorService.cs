using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public interface IInstructorService
    {
        List<InstructorResponse> GetInstructors();
    }
}
