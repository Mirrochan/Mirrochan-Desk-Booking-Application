
using System.Net;
using System.Text.Json;

namespace DeskBookingAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
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
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case InvalidOperationException e when e.Message.Contains("already exists"):
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    default:
                        _logger.LogError(ex, "Unhandled exception");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new
                {
                    statusCode = response.StatusCode,
                    message = ex.Message,
                    details = ex.GetType().Name
                });

                await response.WriteAsync(result);
            }
        }
    }
}