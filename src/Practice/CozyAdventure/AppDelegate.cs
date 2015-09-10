using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyAdventure.Public.Controls;
using CozyAdventure.Public.Controls.Enum;
using CozyAdventure.View.Scene;

namespace CozyAdventure
{
    class AppDelegate : CCApplicationDelegate
    {
        public static CCWindow SharedWindow { get; set; }

        public static CCSize DefaultResolution;

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            SharedWindow = mainWindow;

            DefaultResolution = new CCSize(
                application.MainWindow.WindowSizeInPixels.Width,
                application.MainWindow.WindowSizeInPixels.Height);

            application.ContentRootDirectory = "Content";

            CCScene scene = new HomePageScene();
            mainWindow.RunWithScene(scene);
        }
    }
}
