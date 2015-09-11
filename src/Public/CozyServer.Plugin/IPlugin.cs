using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyServer.Plugin
{
    public interface IPlugin
    {
        void StatusCallback(object server, object msg);
        void DataCallback(object server, object msg);
    }
}
