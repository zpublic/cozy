using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyServer.Plugin;
using Lidgren.Network;
using CozyNetworkHelper;

namespace CozyAdventure.ServerPlugin
{
    public class AdventurePlugin : IPlugin
    {
        public void OnEnter()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAdventure.Protocol");
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
            uint id = msg.ReadUInt32();
            var m = MessageReader.GetMessageInstance(id, msg);
        }
    }
}
