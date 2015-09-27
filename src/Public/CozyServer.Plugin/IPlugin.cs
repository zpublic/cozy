using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyServer.Plugin
{
    public interface IPlugin
    {
        void StatusCallback(object msg);
        void DataCallback(object msg);

        void OnEnter(object server);
    }
}
