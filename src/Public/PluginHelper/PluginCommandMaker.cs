using System.Collections.Generic;
using System.Reflection;

namespace PluginHelper
{
    public class PluginCommandMaker
    {
        private Dictionary<string, MethodInfo> MakerDictionary = new Dictionary<string, MethodInfo>();

        public void LoadCommand(Assembly asm, string pluginName)
        {
            var pluginType  = asm.GetType(pluginName);
            var methods     = pluginType.GetMethods();
            foreach (var method in methods)
            {
                if (method.IsStatic
                    && method.ReturnType == typeof(string)
                    && method.Name.StartsWith("Make")
                    && method.Name.EndsWith("Command"))
                {
                    var name                = method.Name.Substring(4, method.Name.Length - 11);
                    MakerDictionary[name]   = method;
                }
            }
        }

        public string Invoke(string commandName, object[] parms)
        {
            if (MakerDictionary.ContainsKey(commandName))
            {
                var method = MakerDictionary[commandName];
                var result = method.Invoke(null, parms) as string;
                return result;
            }
            return null;
        }
    }
}
