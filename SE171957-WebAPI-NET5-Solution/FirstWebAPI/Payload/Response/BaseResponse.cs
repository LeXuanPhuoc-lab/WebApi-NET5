using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebAPI.Payload.Response
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        public BaseResponse() { }

        public BaseResponse(int StatusCode, Object Data)
        {
            StatusCode = StatusCode;
            Data = Data;
        }

        public BaseResponse(int StatusCode, string Message, Object Data)
        {
            StatusCode = StatusCode;
            Message = Message;
            Data = Data;
        }

    }
}
