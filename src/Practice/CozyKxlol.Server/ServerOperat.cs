using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using CozyKxlol.Network.Msg;

namespace CozyKxlol.Server
{
    public partial class Program
    {
        private static void DispatchPacket(NetServer server, int id, NetIncomingMessage msg)
        {
            List<NetConnection> all = server.Connections;
            all.Remove(msg.SenderConnection);
            if (all.Count > 0)
            {
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(id);
                om.Write(msg);
                server.SendMessage(om, all, NetDeliveryMethod.Unreliable, 0);
            }
        }

        private static bool ProcessPacket(NetServer server, int id, NetIncomingMessage msg)
        {
            if (id == MsgId.AccountReg)
            {
                OnProcessAccountReg(server, id, msg);
                return true;
            }
            else if (id == MsgId.AgarLogin)
            {
                OnProcessLogin(server, id, msg);
                return true;
            }
            else if (id == MsgId.AgarPlayInfo)
            {
                return OnProcessPlayerInfo(server, id, msg);
            }
            else if (id == MsgId.AgarBorn)
            {
                OnProcessBorn(server, id, msg);
                return true;
            }
            else if (id == MsgId.HappyPlayerLogin)
            {
                OnProcessHappyLogin(server, id, msg);
                return true;
            }
            else if(id == MsgId.HappyPlayerMove)
            {
                return OnProgressHappyPlayerMove(server, id, msg);
            }

            return false;
        }

        private static void SendMessage(NetServer server, MsgBase msg)
        {
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(msg.Id);
            msg.W(om);
            server.SendToAll(om, NetDeliveryMethod.Unreliable);
        }

        private static void SendMessage(NetServer server, MsgBase msg, NetConnection conn)
        {
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(msg.Id);
            msg.W(om);
            server.SendMessage(om, conn, NetDeliveryMethod.Unreliable, 0);
        }

        private static void SendMessageExceptOne(NetServer server,MsgBase msg, NetConnection except)
        {
            NetOutgoingMessage oom = server.CreateMessage();
            oom.Write(msg.Id);
            msg.W(oom);

            List<NetConnection> all = server.Connections;
            if(all.Contains(except))
            {
                all.Remove(except);
            }
            if (all.Count > 0)
            {
                server.SendMessage(oom, all, NetDeliveryMethod.Unreliable, 0);
            }
        }
    }
}
