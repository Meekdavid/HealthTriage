using Common.Models;
using Core.Results;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Security;
using System.Text.Json;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unhandled exception has occurred on {GetClassName(ex)} - {GetMethodName(ex)} : {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            //context.Response.StatusCode = GetStatusCode(exception);
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            var isDevelopmentOrTest = _hostEnvironment.IsDevelopment() || _hostEnvironment.IsEnvironment("Test");
            var message = exception.Message ?? "An unexpected error occurred. Please try again later.";
            var details = isDevelopmentOrTest ? exception.StackTrace : null;

            IDataResult<Exception> result = new ErrorDataResult<Exception>(exception, StatusCode_ExceptionError, message);
            
            var json = JsonConvert.SerializeObject(result);
            await context.Response.WriteAsync(json);
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                FormatException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                SecurityException => (int)HttpStatusCode.Forbidden,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                FileNotFoundException => (int)HttpStatusCode.NotFound,
                DirectoryNotFoundException => (int)HttpStatusCode.NotFound,
                NotImplementedException => (int)HttpStatusCode.NotImplemented,
                NotSupportedException => (int)HttpStatusCode.MethodNotAllowed,
                TimeoutException => (int)HttpStatusCode.RequestTimeout,
                OperationCanceledException => (int)HttpStatusCode.RequestTimeout,
                InvalidOperationException => (int)HttpStatusCode.Conflict,
                IOException => (int)HttpStatusCode.InternalServerError,
                SqlException => (int)HttpStatusCode.InternalServerError,
                DbUpdateException => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };

        }

        private string GetClassName(Exception exception)
        {
            return exception.TargetSite?.DeclaringType?.Name ?? "UnknownClass";
        }

        private string GetMethodName(Exception exception)
        {
            return exception.TargetSite?.Name ?? "UnknownMethod";
        }
    }
}
