using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Kxlol.Object.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Scene
{
    public partial  class HappinessGameLayer : CozyLayer
    {
        public CozyTiledMap Tileds { get; set; }
        private string DataPath = @".\Content\Data.db";

        public CozyTileSprite Player { get; set; }

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

            LoadMap();

            InitKeyboard();

            Player = CozyTileSprite.Create(@"player");
            Player.Position = Player.ContentSize / 2;
            this.AddChind(Player, 1);
            return true;
        }

        private void LoadMap()
        {
            var loader = new CozyTiledDataLoader(DataPath);
            loader.Load(Tileds);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            keyboard.Update(gameTime);

            var dire = DirectionNow();
            if (dire != Interface.MoveDirection.Unknow)
            {
                Player.Move(dire);
            }
        }
    }
}
