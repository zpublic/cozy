using System;
using System.Collections.Generic;
using System.Reflection;

namespace PluginHelper
{
    public static class ArgsFactoryLoader
    {
        public static List<Tuple<string, Type>> LoadArgsFactory(Assembly asm, string ns)
        {
            var factoryList = new List<Tuple<string, Type>>();
            foreach (var t in asm.GetTypes())
            {
                if (t.Namespace == ns)
                {
                    var name = t.Name.Substring(0, t.Name.Length - 11);
                    factoryList.Add(Tuple.Create(name, t));
                }
            }
            return factoryList;
        }
    }
}
