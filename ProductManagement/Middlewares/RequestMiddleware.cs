using ProductManagement.Core.CustomExceptions;

namespace ProductManagement.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMiddleware> _logger;

        public RequestMiddleware(RequestDelegate next, ILogger<RequestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Incoming HTTP request: {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                await _next(context);

                _logger.LogInformation("Outgoing HTTP response: {StatusCode} {Method} {Path}",
                    context.Response.StatusCode, context.Request.Method, context.Request.Path);
            }
            catch (ProductManagementException ex)
            {
                _logger.LogWarning(ex, "ProductManagementException occurred: {Message}", ex.Message);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    error = "Business validation error",
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception during request processing.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    error = "Internal server error",
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
