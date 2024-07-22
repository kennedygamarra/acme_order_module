using System.Net;
using System.Text.Json;
using System.Xml;

namespace acme.order.api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (XmlException ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            string? data = null;

            var response = new
            {
                Message = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
