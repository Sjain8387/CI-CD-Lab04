using Microsoft.Extensions.Logging;

namespace MyFunctionApp.Services.Logging
{
    public interface ILoggingService
    {
        public void Log(string message);
    }
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
