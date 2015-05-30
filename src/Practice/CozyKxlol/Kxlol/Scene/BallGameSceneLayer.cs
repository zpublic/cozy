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
        Dictionary<uint, CozyCircle> FoodList       = new Dictionary<uint, CozyCircle>();
        Dictionary<uint, UserCircle> CircleList     = new Dictionary<uint, UserCircle>();
        List<KeyValuePair<string, int>> MarkList    = new List<KeyValuePair<string, int>>();

        KeyboardEvents keyboard;

#if EnableMouse
        MouseEvents mouse;
#endif

        NetClientHelper client                  = new NetClientHelper();
        public UserCircle Player                = null;
        public CozyLabel ScoreShow              = null;
        public uint Uid                         = 0;
        public bool IsConnect                   = false;
        private static Random RandomMaker       = new Random();
        private string Name                     = null;
        private int DefaultRadius               = 0;
        private Point MapSize                   = Point.Zero;

        public const int PlayerZOrder           = 2;
        public const int OtherPlayerZOrder      = 1;
        public const int FoodZOrder             = 0;

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
                    IsConnect       = true;
                    var loginMsg    = new Msg_AgarLogin();

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
                    var selfMsg     = (Msg_AgarLoginRsp)b;
                    Uid             = selfMsg.Uid;
                    int X_Size      = selfMsg.Width;
                    int Y_Size      = selfMsg.Height;
                    MapSize         = new Point(X_Size, Y_Size);

                    string RdName   = "TestName-" + RandomMaker.NextString(5);
                    var bornMsg     = new Msg_AgarBorn();
                    bornMsg.UserId  = Uid;
                    bornMsg.Name    = RdName;
                    Name            = RdName;

                    client.SendMessage(bornMsg);
                }
                else if(b.Id == MsgId.AgarFixedBall)
                {
                    var selfMsg = (Msg_AgarFixedBall)b;
                    uint id     = selfMsg.BallId;
                    if(selfMsg.Operat == Msg_AgarFixedBall.Add)
                    {
                        var food        = new DefaultFoodCircle(
                            new Vector2(selfMsg.X, selfMsg.Y), 
                            selfMsg.Radius, selfMsg.Color);

                        FoodList[id]    = food;
                        this.AddChind(food, FoodZOrder);
                    }
                    else if(selfMsg.Operat == Msg_AgarFixedBall.Remove)
                    {
                        CozyCircle food = FoodList[id];
                        this.RemoveChild(food);
                        FoodList.Remove(id);
                    }

                }
                else if(b.Id == MsgId.AgarPlayInfo)
                {
                    var selfMsg = (Msg_AgarPlayInfo)b;
                    uint id     = selfMsg.UserId;
                    if(selfMsg.Operat == Msg_AgarPlayInfo.Add)
                    {
                        var player  = new DefaultUserCircle(
                            new Vector2(selfMsg.X, selfMsg.Y), 
                            selfMsg.Radius, 
                            selfMsg.Color,
                            selfMsg.Name);

                        CircleList[id] = player;
                        this.AddChind(player, OtherPlayerZOrder);
                    }
                    else if(selfMsg.Operat == Msg_AgarPlayInfo.Remove)
                    {
                        if (!CircleList.ContainsKey(id)) return;
                        var player = CircleList[id];
                        this.RemoveChild(player);
                        CircleList.Remove(id);
                    }
                    else if(selfMsg.Operat == Msg_AgarPlayInfo.Changed)
                    {
                        if (!CircleList.ContainsKey(id)) return;
                        uint tag                    = selfMsg.Tag;
                        var player                  = CircleList[id];
                        if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.POSITION_TAG))
                        {
                            player.Position         = new Vector2(selfMsg.X, selfMsg.Y);
                        }
                        if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.RADIUS_TAG))
                        {
                            player.Radius           = selfMsg.Radius;
                        }
                        if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.COLOR_TAG))
                        {
                            player.ColorProperty    = selfMsg.Color.ToColor();
                        }
                        if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.NAME_TAG))
                        {
                            player.Name             = selfMsg.Name;
                        }
                    }
                }
                else if(b.Id == MsgId.AgarFixBallPack)
                {
                    var selfMsg = (Msg_AgarFixBallPack)b;
                    foreach(var obj in selfMsg.FixedList)
                    {
                        uint fid        = obj.Item1;
                        var food        = new DefaultFoodCircle(new Vector2(obj.Item2, obj.Item3), obj.Item4, obj.Item5);
                        FoodList[fid]   = food;
                        this.AddChind(food, FoodZOrder);
                    }
                } 
                else if(b.Id == MsgId.AgarPlayInfoPack)
                {
                    var selfMsg = (Msg_AgarPlayInfoPack)b;
                    foreach(var obj in selfMsg.PLayerList)
                    {
                        uint pid        = obj.Item1;
                        var player      = new DefaultUserCircle(
                            new Vector2(obj.Item2, obj.Item3),
                            obj.Item4,
                            obj.Item5,
                            obj.Item6);

                        CircleList[pid] = player;
                        this.AddChind(player, OtherPlayerZOrder);
                    }
                }
                else if(b.Id == MsgId.AgarSelf)
                {
                    var selfMsg = (Msg_AgarSelf)b;

                    if(selfMsg.Operat == Msg_AgarSelf.Born)
                    {
                        float x = selfMsg.X;
                        float y = selfMsg.Y;
                        int r   = selfMsg.Radius;
                        uint c  = selfMsg.Color;

                        DefaultRadius   = r;
                        Player          = new DefaultUserCircle(new Vector2(x, y), r, c, Name);

                        this.AddChind(Player, PlayerZOrder);

                        if(ScoreShow != null)
                            ScoreShow.Text = String.Format("Score : {0}", Player.Radius - DefaultRadius);
                    }
                    else if(selfMsg.Operat == Msg_AgarSelf.GroupUp)
                    {
                        Player.Radius = selfMsg.Radius;
                        if (ScoreShow != null)
                            ScoreShow.Text = String.Format("Score : {0}", Player.Radius - DefaultRadius);
                    }
                    else if(selfMsg.Operat == Msg_AgarSelf.Dead)
                    {
                        this.RemoveChild(Player);
                        Player = null;

                        // 玩家重生
                        string RdName   = "TestName-" + RandomMaker.NextString(5);
                        var bornMsg     = new Msg_AgarBorn();
                        bornMsg.UserId  = Uid;
                        bornMsg.Name    = RdName;
                        Name            = RdName;

                        client.SendMessage(bornMsg);
                    }
                }
                else if(b.Id == MsgId.AgarMarkListPark)
                {
                    var selfMsg = (Msg_AgarMarkListPack)b;
                    MarkList.Clear();

                    foreach(var obj in selfMsg.MarkList)
                    {
                        if(CircleList.ContainsKey(obj.Key))
                        {
                            MarkList.Add(new KeyValuePair<string, int>(CircleList[obj.Key].Name, obj.Value));
                        }
                        else if(obj.Key == Uid && Player != null)
                        {
                            MarkList.Add(new KeyValuePair<string, int>(Player.Name, obj.Value));
                        }
                    }
                    // 暂时只接受数据不显示
                }
            };

            ScoreShow = new CozyLabel("Score : 0", Color.Red);
            ScoreShow.AnchorPoint = Vector2.Zero;
            this.AddChind(ScoreShow, 1000);

            client.Connect("114.215.134.101", 48360);
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

                #region MapSize

                var pos = Player.Position;
                if(pos.X < 0.0f && Player.Speed.X < 0.0f)
                {
                    pos.X = 0.0f;
                }
                else if(pos.X > MapSize.X && Player.Speed.X > 0.0f)
                {
                    pos.X = MapSize.X;
                }
                if(pos.Y < 0.0f && Player.Speed.Y < 0.0f)
                {
                    pos.Y = 0.0f;
                }
                else if(pos.Y > MapSize.Y && Player.Speed.Y > 0.0f)
                {
                    pos.Y = MapSize.Y;
                }
                Player.Position = pos;

                #endregion

                if (IsConnect && Player.Changed)
                {
                    Player.Changed  = false;
                    var msg         = new Msg_AgarPlayInfo();
                    msg.Operat      = Msg_AgarPlayInfo.Changed;
                    msg.UserId      = Uid;
                    msg.Tag         = GameMessageHelper.POSITION_TAG;
                    msg.X           = Player.Position.X;
                    msg.Y           = Player.Position.Y;
                    client.SendMessage(msg);
                }
            }
        }
    }
}
