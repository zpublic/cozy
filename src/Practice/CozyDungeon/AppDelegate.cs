using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyDungeon.Public.Controls;

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

            var b = new CozyColorSampleButton(100, 100, 158, 158)
            {
                NormalColor     = new CCColor4B(255, 0, 0),
                ClickedColor    = new CCColor4B(0, 255, 0),
                Text            = "Hello Bttton",
                HasBorder       = true,
            };

            b.OnClick += () =>
            {
            };

            var list = new CozySampleListView()
            {
                ContentSize = new CCSize(350, 350),
                Orientation = ControlOrientation.Vertical,
                Position    = new CCPoint(100, 100),
                HasBorder   = true,
            };
            layer.AddChild(list);

            list.AddItem(new CozySampleListViewItemSprite(new CCSprite("gold"))

            {
                MarginBottom    = 100,
                MarginTop       = 100,
                HasBorder       = true,
            });
            list.AddItem(b);
            list.AddItem(new CozySampleListViewItemSprite(new CCSprite("gold")));

            layer.AddEventListener(b.EventListener, layer);

            scene.AddChild(layer);

            mainWindow.RunWithScene(scene);
        }
    }
}
