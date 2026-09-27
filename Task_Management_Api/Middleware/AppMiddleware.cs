using System.Net;
using System.Text.Json;

namespace Task_Management_Api.Middleware
{
    public class AppMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AppMiddleware> _logger;

        public AppMiddleware(RequestDelegate next, ILogger<AppMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Unhandled exception: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { message = "Internal Server Error" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}