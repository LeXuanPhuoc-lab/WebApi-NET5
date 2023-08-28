using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Payloads
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
