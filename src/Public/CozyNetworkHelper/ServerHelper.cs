using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using CozyNetworkProtocol;

namespace CozyNetworkHelper
{
    public static class ServerHelper
    {
        public static void SendMessage(this NetServer server, MessageBase msg)
        {
            var om = server.CreateMessage();
            msg.Write(om);
            SendMessage(server, om);
        }

        public static void SendMessage(this NetServer server, MessageBase msg, NetConnection conn)
        {
            var om = server.CreateMessage();
            msg.Write(om);
            SendMessage(server, om, conn);
        }

        public static void SendMessageExceptOne(this NetServer server, MessageBase msg, NetConnection except)
        {
            var om = server.CreateMessage();
            msg.Write(om);
            SendMessageExceptOne(server, msg, except);
        }

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
