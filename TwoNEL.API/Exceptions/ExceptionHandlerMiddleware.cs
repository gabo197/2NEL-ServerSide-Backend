using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace TwoNEL.API.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    ApplicationException e => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException e => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
