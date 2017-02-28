using System;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.ModelBinding;

namespace log_writer.backend
{
    /// <summary>
    /// Логирует полученные сообщения
    /// </summary>
    public class LogWriterModule : NancyModule
    {
        private ILogger _logger;

        public LogWriterModule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogWriterModule>();

            Post("log", args =>
            {
                LogMessage logMessage = this.Bind<LogMessage>();
                _logger.Log(logMessage.Level, (EventId) 0, logMessage.Message, (Exception) null, (state, error) => state.ToString());

                return HttpStatusCode.OK;
            });
        }
    }
}