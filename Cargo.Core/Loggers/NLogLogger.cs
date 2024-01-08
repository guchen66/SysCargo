using NLog.Config;
using NLog.Targets;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cargo.Core.Log
{
    public class NLogLogger : ILogger
    {
        private readonly NLog.Logger _logger;
        private readonly string _logDirectory;

        public NLogLogger(string logDirectory = null)
        {
            _logDirectory = logDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "logs");

            // 初始化Logger实例
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget("fileTarget")
            {
                FileName = Path.Combine(_logDirectory, "${shortdate}.log"),
                Layout = "${longdate} ${uppercase:${level}} ${logger} ${message} ${exception}",
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Date,
                MaxArchiveFiles = 7,
                ArchiveFileName = Path.Combine(_logDirectory, "archives", "${shortdate}.{#}.log")
            };
            config.AddTarget(fileTarget);
            config.AddRuleForAllLevels(fileTarget);

            LogManager.Configuration = config;

            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Log(LogLevel level, string message, Exception exception = null)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    _logger.Trace(exception, message);
                    break;
                case LogLevel.Debug:
                    _logger.Debug(exception, message);
                    break;
                case LogLevel.Info:
                    _logger.Info(exception, message);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(exception, message);
                    break;
                case LogLevel.Error:
                    _logger.Error(exception, message);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(exception, message);
                    break;
                default:
                    _logger.Debug(exception, message);
                    break;
            }
        }
    }
}
