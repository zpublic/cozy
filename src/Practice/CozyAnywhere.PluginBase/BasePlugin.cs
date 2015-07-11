using System;
namespace CozyAnywhere.PluginBase
{
    public abstract class BasePlugin
    {
        public abstract string PluginName { get; }

        public abstract PluginCommandMethodReturnValue Shell(string commandContent);
    }
}