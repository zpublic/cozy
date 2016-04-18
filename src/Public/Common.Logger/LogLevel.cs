using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logger
{
    public enum LogLevel
    {
        ERROR = 0x11170, //70000
        WARN = 0xea60, //60000
        INFO = 0x9c40, //40000
        DEBUG = 0x7530, //30000
    }
}
