using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logger
{
    public interface ILogger
    {
        void Error(string format, params string[] args);

        void Info(string format, params string[] args);

        void Warn(string format, params string[] args);

        void Debug(string format, params string[] args);
    }
}
