using Common.Extensions;
using System.Net;
using System.Text.Json;

namespace DynamicMapEngine.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string code;
            string message;

            if (exception is StatusCodeException sce)
            {
                statusCode = (int)sce.StatusCode;
                code = sce.Code;
                message = sce.UserMessage;
            }
            else
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                code = "3000001";
                message = $"An unhandled exception occured: {exception.Message}";
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                Code = code,
                Message = message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
