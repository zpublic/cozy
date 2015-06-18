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

namespace CozyKxlol.Kxlol.Scene
{
    public partial class BallGameSceneLayer : CozyLayer
    {
        Dictionary<uint, CozyCircle> FoodList       = new Dictionary<uint, CozyCircle>();
        Dictionary<uint, UserCircle> CircleList     = new Dictionary<uint, UserCircle>();
        List<KeyValuePair<string, int>> MarkList    = new List<KeyValuePair<string, int>>();
        KeyboardEvents keyboard;
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
            keyboard.KeyPressed += new EventHandler<KeyboardEventArgs>(OnKeyPressed);
            keyboard.KeyReleased += new EventHandler<KeyboardEventArgs>(OnKeyReleased);
            client.StatusMessage += new EventHandler<NetClientHelper.StatusMessageArgs>(OnStatusMessage);

            client.DataMessage += new EventHandler<NetClientHelper.DataMessageArgs>(OnDataMessage);

            ScoreShow = new CozyLabel("Score : 0", Color.Red);
            ScoreShow.AnchorPoint = Vector2.Zero;
            this.AddChind(ScoreShow, 1000);

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
