using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyServer.Plugin;
using Lidgren.Network;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using CozyAdventure.Protocol.Msg;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin : IPlugin
    {
        public void OnEnter()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAdventure.Protocol");
            MessageCallbackManager.RegisterCallback(this);
        }

        private void RegisterCallback()
        {

        }

        public void StatusCallback(object server, object msg)
        {
            var tempSrv = server as NetServer;
            var tempMsg = msg as NetIncomingMessage;
            if(tempMsg == null || tempSrv == null)
            {
                throw new ArgumentNullException("server or msg is null");
            }
            StatusCallbackImpl(tempSrv, tempMsg);
        }

        public void DataCallback(object server, object msg)
        {
            var tempSrv = server as NetServer;
            var tempMsg = msg as NetIncomingMessage;
            if (tempMsg == null || tempSrv == null)
            {
                throw new ArgumentNullException("server or msg is null");
            }
            DataCallbackImpl(tempSrv, tempMsg);
        }

        public void StatusCallbackImpl(NetServer server, NetIncomingMessage msg)
        {
            
        }

        public void DataCallbackImpl(NetServer server, NetIncomingMessage msg)
        {
            var m = MessageReader.GetMessageInstance(msg);
            MessageCallbackManager.ShellCallback(m, server, msg);
        }

        [CallBack(typeof(RegisterMessage))]
        public void OnRegisterMessage(NetPeer server, NetBuffer buff, MessageBase msg)
        {
            var srv = server as NetServer;
            var im  = buff as NetIncomingMessage;

            if(srv != null && im != null)
            {
                RegisterMessageImpl(srv, im, msg);
            }
        }

        private void RegisterMessageImpl(NetServer server, NetIncomingMessage im, MessageBase msg)
        {
            var r = new RegisterResultMessage()
            {
                Result = "OK",
            };
            server.SendMessage(r, im.SenderConnection);
        }
    }
}
