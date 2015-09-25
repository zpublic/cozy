using CozyAdventure.Protocol.Msg;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.ServerPlugin.Model;
using CozyAdventure.Model;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin
    {
        [CallBack(typeof(RegisterMessage))]
        public void OnRegisterMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                RegisterMessageImpl(srv, im, msg);
            }
        }

        [CallBack(typeof(LoginMessage))]
        public void OnLoginMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                LoginMessageImpl(srv, im, msg);
            }
        }

        [CallBack(typeof(PullMessage))]
        public void OnPullMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                PullMessageImpl(srv, im, msg);
            }
        }

        [CallBack(typeof(GotoMapMessage))]
        public void OnGotoMapMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                GotoMapMessageImpl(srv, im, msg);
            }
        }

        [CallBack(typeof(GotoHomeMessage))]
        public void OnGotoHomeMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                GotoHomeMessageImpl(srv, im, msg);
            }
        }
    }
}
