using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logger
{
    public interface ILoggerImp
    {
        Type LoggerType { get; set; }

        string LogName { get; set; }

        void InitImp();

        void WriteEntry(string message, LogLevel level, int eventId, ushort taskCategory, string eventSource);
    }
}
