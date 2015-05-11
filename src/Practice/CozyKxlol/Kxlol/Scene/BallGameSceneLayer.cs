using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starbound.Input;
using Starbound.UI.Resources;
using Starbound.UI.Controls;
using Starbound.UI.XNA.Resources;
using Starbound.UI.XNA.Renderers;
using CozyKxlol.Network;

namespace CozyKxlol.Kxlol.Scene
{
    class BallGameSceneLayer : CozyLayer
    {
        List<CozyCircle> CircleList = new List<CozyCircle>();
        KeyboardEvents keyboard;
        String sdbg;
        MouseEvents mouse;
        List<Control> controls;
        StackPanel panel;
        XNARenderer renderer;
        NetClientHelper client = new NetClientHelper();

        public BallGameSceneLayer()
        {
            keyboard = new KeyboardEvents();
            keyboard.KeyPressed += (sender, e) =>
            {
                sdbg = String.Format("Key Pressed: " + e.Key + " Modifiers: " + e.Modifiers);
            };
            keyboard.KeyReleased += (sender, e) =>
            {
                sdbg = String.Format("Key Released: " + e.Key + " Modifiers: " + e.Modifiers);
            };

            mouse = new MouseEvents();
            mouse.ButtonClicked += MouseEvents_ButtonClicked;

            var circle1 = new CozyCircle(new Vector2(300.0f, 300.0f), 20.0f, Color.Red, new Vector2(20.0f, 0.0f));
            var circle2 = new CozyCircle(new Vector2(400.0f, 300.0f), 20.0f, Color.Blue, new Vector2(0.0f, 20.0f));
            var circle3 = new CozyCircle(new Vector2(500.0f, 300.0f), 20.0f, Color.Green, new Vector2(-20.0f, 20.0f));
            var circle4 = new CozyCircle(
                new Vector2(500.0f, 200.0f),
                50.0f,
                CozyCircle.RandomColor(),
                new Vector2(-20.0f, 20.0f));

            CircleList.Add(circle1);
            CircleList.Add(circle2);
            CircleList.Add(circle3);
            CircleList.Add(circle4);

            renderer = new XNARenderer();
            controls = new List<Control>();
            panel = new StackPanel() { Orientation = Orientation.Horizontal, ActualWidth = 1280, ActualHeight = 800 };
            panel.UpdateLayout();
        }

        private Random random = new Random();
        void MouseEvents_ButtonClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                client.Connect();
                string text = "";
                for (int index = 0; index < 10; index++)
                {
                    text += (char)random.Next(65, 105);
                }

                panel.AddChild(new Starbound.UI.Controls.Rectangle()
                {
                    PreferredHeight = random.Next(20) + 10,
                    PreferredWidth = random.Next(20) + 10,
                    Margin = new Starbound.UI.Thickness(3, 3, 0, 0),
                    Color = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble())
                });
            }
            else if (e.Button == MouseButton.Right)
            {
                panel.AddChild(new Starbound.UI.Controls.Button()
                {
                    PreferredHeight = random.Next(50) + 50,
                    PreferredWidth = random.Next(50) + 50,
                    Margin = new Starbound.UI.Thickness(3, 3, 0, 0),
                    Font = Starbound.UI.Application.ResourceManager.GetResource<IFontResource>("Font"),
                    Content = "hehe",
                    Background = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble()),
                    Foreground = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble())
                });
                panel.Orientation = panel.Orientation == Orientation.Horizontal ? Orientation.Veritical : Orientation.Horizontal;
            }
        }

        public override void Update(GameTime gameTime)
        {
            client.Update();
            keyboard.Update(gameTime);
            mouse.Update(gameTime);
            foreach (var obj in CircleList)
            {
                obj.Update(gameTime);
            }

            List<KeyValuePair<CozyCircle, CozyCircle>> RemoveList = new List<KeyValuePair<CozyCircle, CozyCircle>>();
            foreach (var obj1 in CircleList)
            {
                foreach (var obj2 in CircleList)
                {
                    if (obj1.CanEat(obj2))
                    {
                        RemoveList.Add(new KeyValuePair<CozyCircle, CozyCircle>(obj1, obj2));
                    }
                }
            }

            foreach (var obj in RemoveList)
            {
                obj.Key.Radius = obj.Key.Radius + obj.Value.Radius;
                CircleList.Remove(obj.Value);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            foreach (Control control in controls)
            {
                renderer.Render(control, spriteBatch);
            }

            foreach (Control control in panel.Children)
            {
                renderer.Render(control, spriteBatch);
            }
            spriteBatch.Begin();

            foreach (var obj in CircleList)
            {
                obj.Draw(gameTime, spriteBatch);
            }
            if (sdbg != null)
            {
                spriteBatch.DrawString(CozyGame.nolmalFont, sdbg, new Vector2(20, 20), Color.Red);
            }
        }
    }
}
