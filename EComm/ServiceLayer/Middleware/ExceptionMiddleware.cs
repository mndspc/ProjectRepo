using Microsoft.AspNetCore.Http.HttpResults;
using ServiceLayer.Exceptions;
using ServiceLayer.Models;
using System.Net;
using System.Text.Json;

namespace ServiceLayer.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext,Exception ex)
        {
            HttpStatusCode status=HttpStatusCode.InternalServerError;
            string message = "Something went wrong";

            switch (ex)
            {
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    message=ex.Message;
                    break;
                case BadRequestException:
                    status = HttpStatusCode.BadRequest;
                    message=ex.Message;
                    break;
            }

            var response = new ErrorResponse
            {
                Message = message,
                StatusCode = Convert.ToInt32(status),
                Timestamp = DateTime.Now
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode=Convert.ToInt32(status);

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
