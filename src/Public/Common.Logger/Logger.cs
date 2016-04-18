using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logger
{
    public class Logger : ILogger
    {
        private static Logger logger;

        private static object syncRoot = new object();

        private static ILoggerImp loggerImp;

        public static bool IsWriteError = true;

        public static bool IsWriteInfo = true;

        public static bool IsWriteWarn = true;

        public static bool IsWriteDebug = true;

        private Logger(Type type)
        {
            loggerImp = new LoggerImp();
            loggerImp.LoggerType = type;
        }

        public static Logger GetInstance(Type type)
        {
            lock (syncRoot)
            {
                if (logger == null)
                {
                    logger = new Logger(type);
                }
            }
            return logger;
        }

        public void Error(string format, params string[] args)
        {
            if (IsWriteError)
            {
                string message = format;
                if (args.Count() > 0)
                {
                    message = string.Format(format, args);
                }
                loggerImp.WriteEntry(message, LogLevel.ERROR, 0, 0, string.Empty);
            }
        }

        public void Info(string format, params string[] args)
        {
            if (IsWriteInfo)
            {
                string message = format;
                if (args.Count() > 0)
                {
                    message = string.Format(format, args);
                }
                loggerImp.WriteEntry(message, LogLevel.INFO, 0, 0, string.Empty);
            }
        }

        public void Warn(string format, params string[] args)
        {
            if (IsWriteWarn)
            {
                string message = format;
                if (args.Count() > 0)
                {
                    message = string.Format(format, args);
                }
                loggerImp.WriteEntry(message, LogLevel.WARN, 0, 0, string.Empty);
            }
        }

        public void Debug(string format, params string[] args)
        {
            if (IsWriteDebug)
            {
                string message = format;
                if (args.Count() > 0)
                {
                    message = string.Format(format, args);
                }
                loggerImp.WriteEntry(message, LogLevel.DEBUG, 0, 0, string.Empty);
            }
        }
    }
}
