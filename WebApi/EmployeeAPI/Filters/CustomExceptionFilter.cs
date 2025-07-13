using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace EmployeeAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(IWebHostEnvironment env, ILogger<CustomExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred");

            // Write to a file
            WriteExceptionToFile(context.Exception);

            // Set result
            var result = new ObjectResult(new
            {
                error = "An error occurred while processing your request",
                detail = context.Exception.Message
            })
            {
                StatusCode = 500
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }

        private void WriteExceptionToFile(Exception exception)
        {
            try
            {
                // Create logs directory if it doesn't exist
                var logDirectory = Path.Combine(_env.ContentRootPath, "logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Create log file path
                var logFile = Path.Combine(logDirectory, $"error-{DateTime.Now:yyyyMMdd}.log");

                // Create log entry
                var sb = new StringBuilder();
                sb.AppendLine($"DateTime: {DateTime.Now}");
                sb.AppendLine($"Exception Type: {exception.GetType().FullName}");
                sb.AppendLine($"Message: {exception.Message}");
                sb.AppendLine($"Stack Trace: {exception.StackTrace}");
                sb.AppendLine(new string('-', 50));

                // Write to file
                File.AppendAllText(logFile, sb.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing exception to file");
            }
        }
    }
}
