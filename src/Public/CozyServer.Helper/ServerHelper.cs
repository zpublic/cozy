using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace CozyServer.Helper
{
    public static class ServerHelper
    {
        public static void SendMessage(this NetServer server, NetOutgoingMessage om)
        {
            server.SendToAll(om, NetDeliveryMethod.Unreliable);
        }

        public static void SendMessage(this NetServer server, NetOutgoingMessage om, NetConnection conn)
        {
            server.SendMessage(om, conn, NetDeliveryMethod.Unreliable);
        }

        public static void SendMessageExceptOne(this NetServer server, NetOutgoingMessage om, NetConnection except)
        {
            List<NetConnection> all = server.Connections;
            if (all.Contains(except))
            {
                all.Remove(except);
            }
            if (all.Count > 0)
            {
                server.SendMessage(om, all, NetDeliveryMethod.Unreliable, 0);
            }
        }
    }
}
