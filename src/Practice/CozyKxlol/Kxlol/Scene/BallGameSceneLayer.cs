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
using CozyKxlol.Network.Msg;
using CozyKxlol.Kxlol.Extends;

namespace CozyKxlol.Kxlol.Scene
{
    class BallGameSceneLayer : CozyLayer
    {
        List<CozyCircle> RenderList             = new List<CozyCircle>();
        Dictionary<uint, CozyCircle> FoodList   = new Dictionary<uint, CozyCircle>();
        Dictionary<uint, CozyCircle> CircleList = new Dictionary<uint, CozyCircle>();

        KeyboardEvents keyboard;
        String sdbg;
        MouseEvents mouse;
        List<Control> controls;
        StackPanel panel;
        XNARenderer renderer;
        NetClientHelper client                  = new NetClientHelper();
        public CozyCircle Player                = null;
        public uint Uid                         = 0;
        public bool IsConnect                   = false;

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
                        if(Player != null)
                        {
                            Player.OnKeyPressd(sender, e);
                        }
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
                        if(Player != null)
                        {
                            Player.OnKeyReleased(sender, e);
                        }
                        break;
                    default:
                        sdbg = String.Format("Key Released: " + e.Key + " Modifiers: " + e.Modifiers);
                        break;
                }
            };

            mouse       = new MouseEvents();
#if MouseDebug
            mouse.ButtonClicked += MouseEvents_ButtonClicked;
#endif
            renderer    = new XNARenderer();
            controls    = new List<Control>();
            panel       = new StackPanel() { Orientation = Orientation.Horizontal, ActualWidth = 1280, ActualHeight = 800 };
            panel.UpdateLayout();

            client.StatusMessage += (sender, msg) =>
            {
                if(msg.Status == ConnectionStatus.Connected)
                {
                    IsConnect = true;
                    var loginMsg = new Msg_AgarLogin();
                    client.SendMessage(loginMsg);
                }
                else if(msg.Status == ConnectionStatus.Disconnected)
                {
                    IsConnect = false;
                }
            };

            client.DataMessage += (sender, msg) =>
            {
                if (!IsConnect) return;

                MsgBase b = msg.Msg;
                if(b.Id == MsgId.AgarLoginRsp)
                {
                    var selfMsg = (Msg_AgarLoginRsp)b;
                    Uid         = selfMsg.Uid;
                    Player      = new DefaultUserCircle(new Vector2(selfMsg.X, selfMsg.Y),selfMsg.Radius, selfMsg.Color);
                    RenderList.Add(Player);
                }
                else if(b.Id == MsgId.AgarFixedBall)
                {
                    var selfMsg = (Msg_AgarFixedBall)b;
                    uint id     = selfMsg.BallId;
                    if(selfMsg.Operat == Msg_AgarFixedBall.Add)
                    {
                        var food        = new DefaultFoodCircle(new Vector2(selfMsg.X, selfMsg.Y), selfMsg.Color);
                        FoodList[id]    = food;
                        RenderList.Add(food);
                    }
                    else if(selfMsg.Operat == Msg_AgarFixedBall.Remove)
                    {
                        CozyCircle food = FoodList[id];
                        RenderList.Remove(food);
                        FoodList.Remove(id);
                    }

                }
                else if(b.Id == MsgId.AgarPlayInfo)
                {
                    var selfMsg = (Msg_AgarPlayInfo)b;
                    uint id     = selfMsg.PlayerId;
                    if(selfMsg.Operat == Msg_AgarPlayInfo.Add)
                    {
                        var player      = new DefaultUserCircle(
                            new Vector2(selfMsg.X, selfMsg.Y), 
                            selfMsg.Radius, 
                            selfMsg.Color);

                        CircleList[id]  = player;
                        RenderList.Add(player);
                    }
                    else if(selfMsg.Operat == Msg_AgarPlayInfo.Remove)
                    {
                        var player = CircleList[id];
                        RenderList.Remove(player);
                        CircleList.Remove(id);
                    }
                    else if(selfMsg.Operat == Msg_AgarPlayInfo.Changed)
                    {
                        var player      = CircleList[id];
                        player.Position = new Vector2(selfMsg.X, selfMsg.Y);
                        player.Radius   = selfMsg.Radius;
                        player.ColorProperty = selfMsg.Color.ToColor();
                    }
                }
            };

            client.Connect("127.0.0.1", 48360);
        }

        private Random random = new Random();
        void MouseEvents_ButtonClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                string text = "";
                for (int index = 0; index < 10; index++)
                {
                    text += (char)random.Next(65, 105);
                }

                panel.AddChild(new Starbound.UI.Controls.Rectangle()
                {
                    PreferredHeight = random.Next(20) + 10,
                    PreferredWidth  = random.Next(20) + 10,
                    Margin          = new Starbound.UI.Thickness(3, 3, 0, 0),
                    Color           = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble())
                });
            }
            else if (e.Button == MouseButton.Right)
            {
                panel.AddChild(new Starbound.UI.Controls.Button()
                {
                    PreferredHeight = random.Next(50) + 50,
                    PreferredWidth  = random.Next(50) + 50,
                    Margin          = new Starbound.UI.Thickness(3, 3, 0, 0),
                    Font            = Starbound.UI.Application.ResourceManager.GetResource<IFontResource>("Font"),
                    Content         = "hehe",
                    Background      = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble()),
                    Foreground      = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble())
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
                obj.Value.Update(gameTime);
            }
            if (Player != null)
            {
                Player.Update(gameTime);
                if(IsConnect && Player.Changed)
                {
                    Player.Changed  = false;
                    var msg         = new Msg_AgarPlayInfo();
                    msg.Operat      = Msg_AgarPlayInfo.Changed;
                    msg.PlayerId    = Uid;
                    msg.X           = Player.Position.X;
                    msg.Y           = Player.Position.Y;
                    msg.Radius      = Player.Radius;
                    msg.Color       = Player.ColorProperty.PackedValue;
                    client.SendMessage(msg);
                }
            }

            if(!IsConnect)
            {
                sdbg = "Cannot Connect!";
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

            foreach (var obj in RenderList)
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
