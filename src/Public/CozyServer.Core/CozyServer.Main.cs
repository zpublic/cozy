using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using CozyServer.Plugin;
using System.Threading;
using System.IO;

namespace CozyServer.Core
{
    public partial class CozyServer
    {
        public const int MaxBlockSize = 8192;

        private NetServer InnerServer { get; set; } 

        public Predicate<string> PluginFilter
        {
            get
            {
                return PluginMgr.PluginFilter;
            }
            set
            {
                PluginMgr.PluginFilter = value;
            }
        }

        public int MaximumConnections
        {
            get
            {
                if (InnerServer == null) throw new NullReferenceException("Server is null");
                return InnerServer.Configuration.MaximumConnections;
            }
            set
            {
                if (InnerServer == null) throw new NullReferenceException("Server is null");
                InnerServer.Configuration.MaximumConnections = value;
            }
        }

        public int Port
        {
            get
            {
                if (InnerServer == null) throw new NullReferenceException("Server is null");
                return InnerServer.Port;
            }
        }

        public bool IsRunning
        {
            get
            {
                return InnerServer.Status == NetPeerStatus.Running;
            }
        }

        public bool IsInitComplete { get; set; }

        public PluginManager PluginMgr { get; set; } = new PluginManager();

        private CozyMessageQueue MessageQueue { get; set; } = new CozyMessageQueue();

        private CozyWorkerSet WorkerSet { get; set; }

        public CozyServer(string ServerName, int MaxConnect, int port)
        {
            if(ServerName == null || ServerName.Length == 0)
            {
                throw new NullReferenceException("Server Name is null");
            }
            var config  = new NetPeerConfiguration(ServerName);
            config.Port = port;
            InnerServer = new NetServer(config);

            Init();
        }

        private void Init()
        {
            InitFilter();
            LoadPlugin();
            InitWorkers();

            IsInitComplete = true;
        }

        private void InitWorkers()
        {
            WorkerSet = new CozyWorkerSet(MessageCallback, 4);
        }

        private void LoadPlugin()
        {
            PluginMgr.LoadPlugins(@"./", InnerServer);
        }

        private void InitFilter()
        {
            PluginMgr.PluginFilter = (name) =>
            {
                return Path.GetFileNameWithoutExtension(name).StartsWith("CozyAdventure");
            };
        }

        public void Listen()
        {
            if(IsInitComplete && IsRunning)
            {
                throw new Exception("Server is already running");
            }
            WorkerSet.WorkStart();
            InnerServer.Start();

        }

        public void Connect(string ip, int port)
        {
            if (IsInitComplete)
            {
                InnerServer.Connect(ip, port);
            }
        }

        public void Shutdown()
        {
            if(IsInitComplete && IsRunning)
            {
                InnerServer.Shutdown("ShutDown");
                WorkerSet.WorkStop();
            }
        }

        public void MessageCallback(NetIncomingMessage msg)
        {
            if(msg != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        OnStatusMessage(msg);
                        break;
                    case NetIncomingMessageType.Data:
                        OnDataMessage(msg);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RecivePacket()
        {
            NetIncomingMessage msg;
            while ((msg = InnerServer.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        InnerServer.SendDiscoveryResponse(null, msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                    case NetIncomingMessageType.Data:
                        WorkerSet.PushMessage(msg);
                        break;
                    default:
                        break;
                }
            }
        }

        public void EnterMainLoop()
        {
            while(InnerServer.Status == NetPeerStatus.Running)
            {
                RecivePacket();
                Thread.Sleep(0);
            }
        }

        private void OnStatusMessage(NetIncomingMessage buff)
        {
            PluginMgr.NotifyStatus(buff);
        }

        private void OnDataMessage(NetIncomingMessage buff)
        {
            PluginMgr.NotifyData(buff);
        }
    }
}
