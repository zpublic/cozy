using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyLua.Plugin
{
    public class CozyLuaPluginBase : ICozyLuaPlugin
    {
        protected IDictionary<string, MethodBase> mMethods= new Dictionary<string, MethodBase>();

        public string GetPluginName()
        {
            return "noname";
        }

        public IEnumerator<KeyValuePair<string, MethodBase>> GetEnumerator()
        {
            return mMethods.GetEnumerator();
        }

        public ICollection<string> GetMethodNames()
        {
            return mMethods.Keys;
        }

        public ICollection<MethodBase> GetMethods()
        {
            return mMethods.Values;
        }
    }
}
