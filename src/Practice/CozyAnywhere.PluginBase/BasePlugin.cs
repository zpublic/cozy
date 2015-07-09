using System;
namespace CozyAnywhere.PluginBase
{
    public abstract class BasePlugin
    {
        public abstract string PluginName { get; }

        public void Dispatch(IPluginCommand command)
        {
            command.Execute(this);
        }

        public void Shell(IPluginCommand command)
        {
            // Unknow Command
            throw new Exception("Unknow Command Type");
        }
    }
}