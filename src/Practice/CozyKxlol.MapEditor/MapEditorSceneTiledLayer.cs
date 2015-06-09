using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor
{
    public class MapEditorSceneTiledLayer : CozyLayer
    {
        // update and show current map from TiledMapDataContainer`s data
        public CozyTiledMap TiledMap { get; set; }

        public MapEditorSceneTiledLayer(Point mapSize, Vector2 nodeSize)
        {
            var size                    = mapSize;
            TiledMap                    = new CozyTiledMap(size);
            TiledMap.NodeContentSize    = nodeSize;

            MapEditorScene.Container.DataChangedMessage += OnDataChanged;
            this.AddChind(TiledMap);

            var thumb   = new MapEditorThumb(mapSize, TiledMap.NodeContentSize / 4);
            this.AddChind(thumb, 1);
        }

        private void OnDataChanged(object sender, 
            TiledMapDataContainer.DataChangedMessageArgs msg)
        {
            TiledMap.Change(msg.X, msg.Y, msg.Data);
        }
    }
}
