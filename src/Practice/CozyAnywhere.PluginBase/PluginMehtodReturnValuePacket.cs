using System.Collections.Generic;

namespace CozyAnywhere.PluginBase
{
    public class PluginMehtodReturnValuePacket
    {
        public int Count
        {
            get
            {
                if (Packet == null)
                {
                    return 0;
                }
                return Packet.Count;
            }
        }

        public List<ReturnValuePacket> Packet { get; set; }
    }
}