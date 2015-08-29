using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyDungeon.Game.Component.Controls;

namespace CozyDungeon
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
            application.ContentSearchPaths.Add("SD");

            CCScene scene = new CCScene(mainWindow);
            CCLayer layer = new IntroLayer(DefaultResolution);

            var b = new CozySampleButton(0, 0, 158, 158)
            {
                NormalSprite    = new CCSprite(@"gold"),
                ClickedSprite   = new CCSprite(@"water"),
                Text            = "Hello Bttton",
                HasBorder       = true,
            };

            b.OnClick += () =>
            {
            };

            layer.AddChild(b);
            layer.AddEventListener(b.EventListener, layer);

            scene.AddChild(layer);

            mainWindow.RunWithScene(scene);
        }
    }
}
