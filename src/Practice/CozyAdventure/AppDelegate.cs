using CocosSharp;
using CozyAdventure.Engine;
using CozyAdventure.Game.Manager;
using CozyAdventure.View.Scene;
using CozyClient.Core;
using CozyNetworkHelper;
using System;

namespace CozyAdventure
{
    internal class AppDelegate : CCApplicationDelegate
    {
        public static CCWindow SharedWindow { get; set; }

        public static CCSize DefaultResolution;

        public static CozyClient.Core.CozyClient SharedClient { get; set; }

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            SharedWindow = mainWindow;

            DefaultResolution = new CCSize(
                application.MainWindow.WindowSizeInPixels.Width,
                application.MainWindow.WindowSizeInPixels.Height);

            application.ContentRootDirectory = "CozyAdventureContent";

            ModuleManager.Instance.Init();
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
        }

        /// <summary>
        /// Manager这里初始化
        /// </summary>
        private void InitManager()
        {
            StringManager.Init();
        }

        private void OnStatusMessage(object sender, ClienEventArgs e)
        {
        }

        public static event EventHandler<Events.MessageReceiveEventArgs> MessageReceiveEventHandler;

        private void OnDataMessage(object sender, ClienEventArgs e)
        {
            var msg = MessageReader.GetMessageInstance(e.Message);

            if (MessageReceiveEventHandler != null)
            {
                MessageReceiveEventHandler(this, new Events.MessageReceiveEventArgs(msg.Id, msg));
            }
        }
    }
}