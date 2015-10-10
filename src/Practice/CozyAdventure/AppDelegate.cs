using CocosSharp;
using Cozy.Game.Manager;
using CozyAdventure.Engine;
using CozyAdventure.Game.Manager;
using CozyAdventure.Protocol;
using CozyAdventure.View.Scene;
using CozyClient.Core;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using System;

namespace CozyAdventure
{
    internal class AppDelegate : CCApplicationDelegate
    {
        public static CCWindow SharedWindow { get; set; }

        public static CCSize DefaultResolution;

        private CozyClient.Core.CozyClient SharedClient { get; set; }

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            SharedWindow = mainWindow;

            DefaultResolution = new CCSize(
                application.MainWindow.WindowSizeInPixels.Width,
                application.MainWindow.WindowSizeInPixels.Height);

            application.ContentRootDirectory = "CozyAdventureContent";

            InitModule();
            InitNetwork();
            InitManager();

            SharedClient.Connect("127.0.0.1", 44360);

            CCScene scene = new LoginScene();
            mainWindow.RunWithScene(scene);
        }

        private void InitNetwork()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAdventure.Protocol");
            SharedClient = new CozyClient.Core.CozyClient("CozyAdventure");
            SharedClient.DataMessage += OnDataMessage;
            SharedClient.StatusMessage += OnStatusMessage;
            MessageManager.RegisterMessage("Client.Send", OnSend);
        }

        /// <summary>
        /// Manager这里初始化
        /// </summary>
        private void InitManager()
        {
            StringManager.Init();
            NetworkMessageManager.Instance.Init();
        }

        /// <summary>
        /// Module这里初始化
        /// </summary>
        private void InitModule()
        {
            ModuleManager.Instance.Init();
        }

        private void OnStatusMessage(object sender, ClienEventArgs e)
        {
            var status = e.Message.ReadByte();
            MessageManager.SendMessage("Client.Status", status);
        }

        private void OnDataMessage(object sender, ClienEventArgs e)
        {
            var msg = MessageReader.GetMessageInstance(e.Message);
            MessageManager.SendMessage("Client.Data", msg);
        }

        private void OnSend(object obj)
        {
            SharedClient.SendMessage((MessageBase)obj);
        }
    }
}