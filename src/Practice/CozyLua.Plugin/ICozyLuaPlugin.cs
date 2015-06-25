using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyLua.Plugin
{
    public interface ICozyLuaPlugin
    {
        string GetPluginName();

        IEnumerator<KeyValuePair<string, MethodBase>> GetEnumerator();

        ICollection<string> GetMethodNames();

        ICollection<MethodBase> GetMethods();
    }
}
