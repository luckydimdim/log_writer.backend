using System;
using Microsoft.Extensions.Logging;

namespace log_writer.backend
{
    /// <summary>
    /// Сообщение для логирования
    /// </summary>
    public class LogMessage
    {
        public String Message = String.Empty;
        public LogLevel Level = LogLevel.None;
    }
}
