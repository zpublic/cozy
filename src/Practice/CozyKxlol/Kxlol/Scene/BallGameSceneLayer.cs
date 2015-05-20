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

#if EnableMouse
        MouseEvents mouse;
#endif

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
#if EnableMouse
            mouse       = new MouseEvents();
            mouse.MouseMoved += (sender, msg) =>
            {
            };
#endif
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
                else if(b.Id == MsgId.AgarFixBallPack)
                {
                    var selfMsg = (Msg_AgarFixBallPack)b;
                    foreach(var obj in selfMsg.FixedList)
                    {
                        uint fid = obj.Item1;
                        var food = new DefaultFoodCircle(new Vector2(obj.Item2, obj.Item3), obj.Item4);
                        FoodList[fid] = food;
                        RenderList.Add(food);
                    }
                } 
                else if(b.Id == MsgId.AgarPlayInfoPack)
                {
                    var selfMsg = (Msg_AgarPlayInfoPack)b;
                    foreach(var obj in selfMsg.PLayerList)
                    {
                        uint pid = obj.Item1;
                        var player = new DefaultUserCircle(
                            new Vector2(obj.Item2, obj.Item3),
                            obj.Item4,
                            obj.Item5);

                        CircleList[pid] = player;
                        RenderList.Add(player);
                    }
                }
                else if(b.Id == MsgId.AgarSelf)
                {
                    var selfMsg = (Msg_AgarSelf)b;
                    uint uid = selfMsg.UserId;

                    if(uid != Uid)
                    {
                        // throw exception
                    }

                    if(selfMsg.Operat == Msg_AgarSelf.GroupUp)
                    {
                        Player.Radius = selfMsg.Radius;
                    }
                    else if(selfMsg.Operat == Msg_AgarSelf.Dead)
                    {
                        // doSomething
                    }
                }
            };

            client.Connect("127.0.0.1", 48360);
        }

        public override void Update(GameTime gameTime)
        {
            client.Update();
            keyboard.Update(gameTime);

#if EnableMouse
            mouse.Update(gameTime);
#endif
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
