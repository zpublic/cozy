using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PluginHelper
{
    public class PluginCommandMaker
    {
        private Dictionary<string, MethodInfo> MakerDictionary = new Dictionary<string, MethodInfo>();

        public void LoadCommand(Assembly asm, string pluginName)
        {
            var pluginType = asm.GetType(pluginName);
            var methods = pluginType.GetMethods();
            foreach (var method in methods)
            {
                if (method.IsStatic
                    && method.ReturnType == typeof(string)
                    && method.Name.StartsWith("Make")
                    && method.Name.EndsWith("Command"))
                {
                    var name = method.Name.Substring(4, method.Name.Length - 11);
                    MakerDictionary[name] = method;
                }
            }
        }
    }
}
