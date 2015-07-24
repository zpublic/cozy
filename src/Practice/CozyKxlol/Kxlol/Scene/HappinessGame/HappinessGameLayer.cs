using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Network.Msg.Happy;
using CozyKxlol.Kxlol.Converter;
using CozyKxlol.Kxlol.Object.Tiled;
using CozyKxlol.Kxlol.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Scene
{
    public partial  class HappinessGameLayer : CozyLayer
    {
        public CozyTiledMap Tileds { get; set; }
        private string DataPath = @".\Content\Data.db";

        public CozyTileSprite Player { get; set; }

        Dictionary<uint, CozyTileSprite> OtherPlayerList = new Dictionary<uint, CozyTileSprite>();

        HappinessPlayerTextureManager PlayerTextureMgr = new HappinessPlayerTextureManager();

        public static HappinessGameLayer Create()
        {
            var layer = new HappinessGameLayer();
            layer.Init();
            return layer;
        }

        public Point MapSize { get { return Tileds.TiledMapSize; } }

        public virtual bool Init()
        {
            Tileds                  = new CozyTiledMap(new Point(20, 15));
            Tileds.NodeContentSize  = Vector2.One * 32;
            this.AddChind(Tileds);

            LoadContent();

            InitKeyboard();
            RegisterClientEvent();

            client.Connect("127.0.0.1", 36048);
            return true;
        }

        private void LoadContent()
        {
            LoadMap();
            LoadPlayerResource();
        }

        private void LoadMap()
        {
            var loader = new CozyTiledDataLoader(DataPath);
            loader.Load(Tileds);
        }

        private void LoadPlayerResource()
        {
            for(uint i = 0; i < 83; ++i)
            {
                var texture = CozyDirector.Instance.TextureCacheInstance.AddImage(@"Pokemon\Pokemon_" + i.ToString("D3"));
                PlayerTextureMgr.Add(i, texture);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            keyboard.Update(gameTime);
            client.Update();

            var dire = DirectionNow();
            if (dire != Interface.MoveDirection.Unknow)
            {
                if(!Player.Moving)
                {
                    Player.Move(dire);
                    var offsetPos = MoveDirectionToPointConverter.MoveDirectionConvertToPoint(dire);

                    var MoveMsg = new Msg_HappyPlayerMove();
                    MoveMsg.Uid = Uid;
                    MoveMsg.X   = offsetPos.X;
                    MoveMsg.Y   = offsetPos.Y;
                    client.SendMessage(MoveMsg);
                }
            }
        }
    }
}
