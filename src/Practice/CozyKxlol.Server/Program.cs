using CozyKxlol.Network.Msg;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyKxlol.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections = 10000;
            config.Port = 48360;

            NetServer server = new NetServer(config);
            server.Start();

            while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
            {
                NetIncomingMessage msg;
                while ((msg = server.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.DiscoveryRequest:
                            server.SendDiscoveryResponse(null, msg.SenderEndPoint);
                            break;
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                            Console.WriteLine(msg.ReadString());
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                            if (status == NetConnectionStatus.Connected)
                            {
                                Console.WriteLine(NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " connected!");
                            }
                            break;
                        case NetIncomingMessageType.Data:
                            if (!ProcessPacket(server, msg))
                            {
                                DispatchPacket(server, msg);
                            }
                            break;
                    }
                }
                Thread.Sleep(1);
            }
            server.Shutdown("app exiting");
        }

        private static void DispatchPacket(NetServer server, NetIncomingMessage msg)
        {
            List<NetConnection> all = server.Connections;
            all.Remove(msg.SenderConnection);
            if (all.Count > 0)
            {
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(msg);
                server.SendMessage(om, all, NetDeliveryMethod.Unreliable, 0);
            }
        }

        private static bool ProcessPacket(NetServer server, NetIncomingMessage msg)
        {
            MsgBase m = new MsgBase();
            m.R(msg);
            if (m.Id == MsgId.AccountReg)
            {
                Msg_AccountReg r = new Msg_AccountReg();
                r.R(msg);
                Msg_AccountRegRsp rr = new Msg_AccountRegRsp();
                rr.suc = true;
                rr.detail = r.name + r.pass;
                NetOutgoingMessage om = server.CreateMessage();
                rr.W(om);
                server.SendMessage(om, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                return true;
            }
            return false;
        }
    }
}
