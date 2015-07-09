using System;

namespace CozyAnywhere.Protocol
{
    public class PluginCommandMethodPacket
    {
        public Func<IPluginCommandMethodArgs, object> Function { get; set; }

        public IPluginCommandMethodArgsFactory ArgsFactory { get; set; }

        public static PluginCommandMethodPacket Create(
            Func<IPluginCommandMethodArgs, object> func, 
            IPluginCommandMethodArgsFactory factory)
        {
            var packet          = new PluginCommandMethodPacket();
            packet.Function     = func;
            packet.ArgsFactory  = factory;
            return packet;
        }
    }
}