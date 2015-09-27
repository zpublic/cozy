using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyServer.Plugin;
using Lidgren.Network;
using CozyNetworkHelper;
using System.Timers;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin : IPlugin
    {
        public NetServer SharedServer { get; set; }

        public AdventurePlugin()
        {
            InitFarm();
        }

        public void OnEnter(object server)
        {
            SharedServer = (NetServer)server;
            MessageReader.RegisterTypeWithAssembly("CozyAdventure.Protocol");
            MessageCallbackManager.RegisterCallback(this);
        }

        public void StatusCallback(object msg)
        {
            var tempMsg = msg as NetIncomingMessage;
            if(tempMsg == null)
            {
                throw new ArgumentNullException("server or msg is null");
            }
            StatusCallbackImpl(SharedServer, tempMsg);
        }

        public void DataCallback(object msg)
        {
            var tempMsg = msg as NetIncomingMessage;
            if (tempMsg == null)
            {
                throw new ArgumentNullException("server or msg is null");
            }
            DataCallbackImpl(SharedServer, tempMsg);
        }

        public void StatusCallbackImpl(NetServer server, NetIncomingMessage msg)
        {
            
        }

        public void DataCallbackImpl(NetServer server, NetIncomingMessage msg)
        {
            var m = MessageReader.GetMessageInstance(msg);
            MessageCallbackManager.ShellCallback(m, msg);
        }
    }
}
