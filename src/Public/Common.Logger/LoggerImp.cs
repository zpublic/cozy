using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Logger
{
    public class LoggerImp : ILoggerImp
    {
        public Type LoggerType { get; set; }

        public string LogName
        {
            get
            {
                return log4net.GlobalContext.Properties["ProcessName"] as string;
            }
            set
            {
                if (value != null)
                {
                    log4net.GlobalContext.Properties["ProcessName"] = value;
                }
            }
        }

        public void InitImp()
        {
            this.LogName = Process.GetCurrentProcess().ProcessName + ".exe";
        }

        public LoggerImp()
        {
            this.InitImp();
        }

        public void WriteEntry(string message, LogLevel level, int eventId, ushort taskCategory, string eventSource)
        {
            ILog log4NetLog = log4net.LogManager.GetLogger(this.LoggerType);
            LoggingEvent loggingEntry = new LoggingEvent(this.LoggerType, log4NetLog.Logger.Repository, this.LogName, new Level((int)level, level.ToString()), message, null);
            loggingEntry.Properties["EventID"] = eventId;
            loggingEntry.Properties["EventMessage"] = message;
            loggingEntry.Properties["TaskCategory"] = taskCategory;
            loggingEntry.Properties["EventSource"] = eventSource;
            log4NetLog.Logger.Log(loggingEntry);

        }
    }
}
