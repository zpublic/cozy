using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class PluginQueryMessage : IMessage
    {
        public uint Id { get { return MessageId.PluginQueryMessage; } }

        public List<string> Plugins { get; set; }

        public void Write(NetBuffer om)
        {
            if(Plugins != null)
            {
                om.Write(Plugins.Count);
                foreach (var obj in Plugins)
                {
                    om.Write(obj);
                }
            }
            else
            {
                om.Write(0);
            }
        }

        public void Read(NetBuffer im)
        {
            if (Plugins == null)
            {
                Plugins = new List<string>();
            }

            int count = im.ReadInt32();
            for(int i = 0; i < count; ++i)
            {
                var plugin = im.ReadString();
                Plugins.Add(plugin);
            }
        }
    }
}
