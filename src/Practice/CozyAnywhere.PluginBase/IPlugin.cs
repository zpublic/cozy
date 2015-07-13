using System;
namespace CozyAnywhere.PluginBase
{
    public interface IPlugin
    {
        string PluginName { get; }

        PluginCommandMethodReturnValue Shell(string commandContent);
    }
}