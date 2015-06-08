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

        public MapEditorThumb(Vector2 nodeSize)
        {
            var size = MapEditorSceneLayer.Container.MapSize;
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
    }
}
