using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.MapEditor.Event;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor.TiledLayer
{
    public class MapEditorThumb : CozyNode
    {
        public CozyTiledMap TiledMap { get; set; }

        public override Vector2 ContentSize
        {
            get
            {
                return TiledMap.TiledMapSize.ToVector2() * TiledMap.NodeContentSize;
            }
            set
            {
                //base.ContentSize = value;
            }
        }

        public MapEditorThumb(Point mapSize, Vector2 nodeSize)
        {
            var size = mapSize;
            TiledMap = new CozyTiledMap(size);
            TiledMap.NodeContentSize = nodeSize;

            MapEditorScene.Container.DataMessage    += OnDataChanged;
            MapEditorScene.Container.ClearMessage   += OnClear;
            this.AddChind(TiledMap);
        }

        private void OnDataChanged(object sender, TiledDataMessageArgs msg)
        {
            TiledMap.Change(msg.X, msg.Y, msg.Data);
        }
        private void OnClear(object sender, TiledClearMessageArgs msg)
        {
            TiledMap.Clear();
        }

        protected override void DrawSelf(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.DrawSelf(gameTime, spriteBatch);
            spriteBatch.DrawRectangle(GlobalPosition + Transform, ContentSize, Color.White, 1);
        }
    }
}
