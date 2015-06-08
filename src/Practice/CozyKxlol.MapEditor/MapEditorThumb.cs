using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor
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

            MapEditorSceneLayer.Container.DataChangedMessage += OnDataChanged;
            this.AddChind(TiledMap);
        }

        private void OnDataChanged(object sender, 
            TiledMapDataContainer.DataChangedMessageArgs msg)
        {
            TiledMap.Change(msg.X, msg.Y, msg.Data);
        }

        protected override void DrawSelf(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.DrawSelf(gameTime, spriteBatch);
            spriteBatch.DrawRectangle(GlobalPosition + Transform, ContentSize, Color.White, 1);
        }
    }
}
