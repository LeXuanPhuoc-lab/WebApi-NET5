using FPTManager.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DemoMiddlewareController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int Id)
        {
            if(Id == 1) {
                int a = 5;
                int b = a / 0;
            }
            else if (Id == 2)
            {
                throw new NotFoundException("Record does not found");
            }
            else
            {
                throw new BadRequestException("Bad Request");
            }

            return Ok();
        }
    }
}
