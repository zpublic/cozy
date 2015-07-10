using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandArgsDispatch
    {
        object Dispatch(PluginCommandMethodArgs args);

        object Shell(PluginCommandMethodArgs args);
    }
}
