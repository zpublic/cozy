using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandArgsDispatch
    {
        string Dispatch(PluginCommandMethodArgs args);

        string Shell(PluginCommandMethodArgs args);
    }
}
