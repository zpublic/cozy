using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Starbound.Input;
using Starbound.UI.Resources;
using Starbound.UI.Controls;
using Starbound.UI.XNA.Resources;
using Starbound.UI.XNA.Renderers;
using CozyKxlol.Network;
using CozyKxlol.Kxlol.Impl;

namespace CozyKxlol.Kxlol.Scene
{
    class BallGameSceneLayer : CozyLayer
    {
        List<CozyCircle> CircleList = new List<CozyCircle>();
        List<CozyCircle> FoodList   = new List<CozyCircle>();
        KeyboardEvents keyboard;
        String sdbg;
        MouseEvents mouse;
        List<Control> controls;
        StackPanel panel;
        XNARenderer renderer;
        NetClientHelper client      = new NetClientHelper();
        public IControlAble Player  = null;

        public BallGameSceneLayer()
        {
            keyboard = new KeyboardEvents();
            keyboard.KeyPressed += (sender, e) =>
            {
                switch(e.Key)
                {
                    case Keys.W:
                    case Keys.S:
                    case Keys.A:
                    case Keys.D:
                        Player.OnKeyPressd(sender, e);
                        break;
                    default:
                        sdbg = String.Format("Key Pressed: " + e.Key + " Modifiers: " + e.Modifiers);
                        break;
                }
            };
            keyboard.KeyReleased += (sender, e) =>
            {
                switch (e.Key)
                {
                    case Keys.W:
                    case Keys.S:
                    case Keys.A:
                    case Keys.D:
                        Player.OnKeyResleased(sender, e);
                        break;
                    default:
                        sdbg = String.Format("Key Released: " + e.Key + " Modifiers: " + e.Modifiers);
                        break;
                }
            };

            mouse = new MouseEvents();
            mouse.ButtonClicked += MouseEvents_ButtonClicked;

            var circle1 = new DefaultUserCircle(new Vector2(300.0f, 300.0f));
            var circle2 = new DefaultUserCircle(new Vector2(400.0f, 300.0f));
            var circle3 = new DefaultUserCircle(new Vector2(500.0f, 300.0f));
            var circle4 = new DefaultUserCircle(new Vector2(500.0f, 200.0f));

            CircleList.Add(circle1);
            CircleList.Add(circle2);
            CircleList.Add(circle3);
            CircleList.Add(circle4);

            Player = circle4;

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

            List<KeyValuePair<CozyCircle, CozyCircle>> RemoveUserList = new List<KeyValuePair<CozyCircle, CozyCircle>>();
            List<KeyValuePair<CozyCircle, CozyCircle>> RemoveFoodList = new List<KeyValuePair<CozyCircle, CozyCircle>>();

            foreach (var obj1 in CircleList)
            {
                foreach (var obj2 in CircleList)
                {
                    if (obj1.CanEat(obj2))
                    {
                        RemoveUserList.Add(new KeyValuePair<CozyCircle, CozyCircle>(obj1, obj2));
                    }
                }

                foreach(var obj2 in FoodList)
                {
                    if(obj1.CanEat(obj2))
                    {
                        RemoveFoodList.Add(new KeyValuePair<CozyCircle, CozyCircle>(obj1, obj2));
                    }
                }
            }

            foreach (var obj in RemoveUserList)
            {
                obj.Key.Radius = obj.Key.Radius + obj.Value.Radius;
                CircleList.Remove(obj.Value);
            }
            foreach (var obj in RemoveFoodList)
            {
                obj.Key.Radius = obj.Key.Radius + 1;
                FoodList.Remove(obj.Value);
            }

            Point winSize = CozyDirector.Instance.WindowSize;
            foreach(var obj in CircleList)
            {
                Vector2 newPos = obj.Position;
                if(obj.Position.X < 0.0f && obj.Direction.X < 0.0f)
                {
                    newPos.X = 0.0f;
                }
                else if(obj.Position.X > winSize.X && obj.Direction.Y > 0.0f)
                {
                    newPos.X = winSize.X;
                }

                if (obj.Position.Y < 0.0f && obj.Direction.Y < 0.0f)
                {
                    newPos.Y = 0.0f;
                }
                else if (obj.Position.Y > winSize.Y && obj.Direction.Y > 0.0f)
                {
                    newPos.Y = winSize.Y;
                }
                obj.Position = newPos;
            }

            UpdateFood();
            //sdbg = String.Format("{0} {1} {2} {3}", Player.IsMoving, Player.Position, Player.Direction, Player.MoveDamping);
        }

        private int MaxFoodSize = 20;
        private void UpdateFood()
        {
            while(FoodList.Count < MaxFoodSize)
            {
                FoodList.Add(new DefaultFoodCircle(CozyCircle.RandomPosition()));
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

            foreach(var obj in FoodList)
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
