using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;
using System.Text.Json;

namespace Scheduler.Jobs.Domain
{
    public class ExceptionContext
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Exception Exception { get; set; }

        public object CustomData { get; set; }

        public void Log(ILogger logger, LogLevel logLevel = LogLevel.Critical)
        {
            logger.Log(logLevel, Exception, $"[Exception Id: {Id}] {Exception.Message}");

            if (Exception?.StackTrace != null)
                logger.Log(logLevel, Exception, $"[Exception Id: {Id}] StackTrace: {JsonSerializer.Serialize(Exception.StackTrace)}");

            logger.Log(logLevel, Exception, $"[Exception Id: {Id}] CustomData: {JsonSerializer.Serialize(CustomData)}");
        }

        public static ExceptionContext New(Exception ex, object customData = null)
        {
            return new ExceptionContext()
            {
                Exception = ExceptionDispatchInfo.Capture(ex).SourceException,
                CustomData = customData
            };
        }

        public static ExceptionContext New(string message, Exception ex, object customData = null)
        {
            ex = ExceptionDispatchInfo.Capture(ex).SourceException;

            return new ExceptionContext()
            {
                Exception = new Exception(message, ex),
                CustomData = customData
            };
        }
    }
}
