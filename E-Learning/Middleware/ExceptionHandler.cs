using System.Net;
using System.Text.Json;

namespace E_Learning.Middleware
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;
        public ExceptionHandler(ILogger<ExceptionHandler> logger, IHostEnvironment env, RequestDelegate next)
        {
            _logger = logger;
            _env = env;
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
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ?
                    new ErrorDetials
                    {
                        Message = ex.Message,
                        StatusCode = context.Response.StatusCode,
                        StackError = ex.StackTrace?.ToString()
                    } :
                    new ErrorDetials
                    {
                        Message = ex.Message,
                        StatusCode = context.Response.StatusCode,
                        StackError = "Internal server Error"
                    };

                var option=new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, option);

                await context.Response.WriteAsync(json);

            }
        }
    }
}
