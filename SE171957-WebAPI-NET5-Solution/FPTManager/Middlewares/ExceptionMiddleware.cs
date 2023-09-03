using FPTManager.Exceptions;
using FPTManager.Models.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FPTManager.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong");
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            string message = ex.Message.ToString();
            switch (ex)
            {
                case NotFoundException _:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case BadRequestException _:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case DbUpdateException _:
                    statusCode = StatusCodes.Status500InternalServerError;

                    if (message.ToUpper().Contains("DUPLICATE"))
                    {
                        message = "Username already exist";
                    }
                    break;
            }
            var errorReponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message
            };
            // set content type 
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorReponse));
        }
    }

    // extension method for middleware
    public static class ExceptionMiddlewareExtension 
    { 
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }    
    }
}
