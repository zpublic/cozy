using System;
using System.Globalization;
using System.Runtime;
using log4net;
using log4net.Core;
using log4net.Util;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace CozyLog
{
    public sealed class Log4NetLog : ILog, ILog<Log4NetLog>
    {
        private global::log4net.ILog _logger;

        // ignore Log4NetLog in the call stack
        private static readonly Type _declaringType = typeof(Log4NetLog);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void InitializeFor(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Debug(string message, params object[] formatting)
        {
            if (_logger.IsDebugEnabled) Log(Level.Debug, message, formatting);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Debug(Func<string> message)
        {
            if (_logger.IsDebugEnabled) Log(Level.Debug, message);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Info(string message, params object[] formatting)
        {
            if (_logger.IsInfoEnabled) Log(Level.Info, message, formatting);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Info(Func<string> message)
        {
            if (_logger.IsInfoEnabled) Log(Level.Info, message);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Warn(string message, params object[] formatting)
        {
            if (_logger.IsWarnEnabled) Log(Level.Warn, message, formatting);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Warn(Func<string> message)
        {
            if (_logger.IsWarnEnabled) Log(Level.Warn, message);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Error(string message, params object[] formatting)
        {
            // don't check for enabled at this level
            Log(Level.Error, message, formatting);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Error(Func<string> message)
        {
            // don't check for enabled at this level
            Log(Level.Error, message);
        }

        public void Error(Func<string> message, Exception exception)
        {
            Log(Level.Error, message, exception);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Fatal(string message, params object[] formatting)
        {
            // don't check for enabled at this level
            Log(Level.Fatal, message, formatting);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Fatal(Func<string> message)
        {
            // don't check for enabled at this level
            Log(Level.Fatal, message);
        }

        public void Fatal(Func<string> message, Exception exception)
        {
            Log(Level.Fatal, message, exception);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private void Log(Level level, Func<string> message)
        {
            Log(level, message(), null);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private void Log(Level level, Func<string> message, Exception exception)
        {
            _logger.Logger.Log(_declaringType, level, message(), exception);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private void Log(Level level, string message, params object[] formatting)
        {
            // SystemStringFormat is used to evaluate the message as late as possible. A filter may discard this message.
            _logger.Logger.Log(_declaringType, level, new SystemStringFormat(CultureInfo.CurrentCulture, message, formatting), null);
        }
    }
}
