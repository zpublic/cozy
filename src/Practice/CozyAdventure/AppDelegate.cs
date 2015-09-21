using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyAdventure.Public.Controls;
using CozyAdventure.Public.Controls.Enum;
using CozyAdventure.View.Scene;
using CozyClient.Core;
using CozyAdventure.Engine;
using CozyNetworkHelper;

namespace CozyAdventure
{
    class AppDelegate : CCApplicationDelegate
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
