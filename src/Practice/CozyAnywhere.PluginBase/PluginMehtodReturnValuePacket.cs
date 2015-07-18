using System.Collections.Generic;

namespace CozyAnywhere.PluginBase
{
    public class PluginMehtodReturnValuePacket
    {
        public int Size { get; set; }

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

        public List<byte[]> Packet { get; set; }
    }
}