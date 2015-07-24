using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Protocol
{
    public interface IPluginCommandArgsDispatch
    {
        PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args);

        PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args);
    }
}
