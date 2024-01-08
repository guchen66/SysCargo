using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Log
{
    public class LoggerHelper
    {
        private readonly ILogger _logger;

        public LoggerHelper(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(LogLevel level, LogMessage logMessage, Exception exception = null)
        {
            var message = LogFormat.ErrorFormat(logMessage); // 或者使用其他的日志格式化方法
            _logger.Log(level, message, exception);
        }
    }
}
