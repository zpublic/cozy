using System;
namespace CozyAnywhere.PluginBase
{
    public abstract class BasePlugin
    {
        public abstract string PluginName { get; }

        public abstract string Shell(string commandContent);
    }
}