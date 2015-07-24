using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.MapEditor.Event;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor.Gui.TiledLayer
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

            MapEditorScene.Container.DataMessage    += OnDataChanged;
            MapEditorScene.Container.ClearMessage   += OnClear;
            MapEditorScene.Container.RefreshMessage += OnRefersh;
            this.AddChind(TiledMap);

            var thumb   = new MapEditorThumb(mapSize, TiledMap.NodeContentSize / 4);
            thumb.Position = new Vector2(thumb.Position.X + 960, thumb.Position.Y);
            this.AddChind(thumb, 1);
        }

        private void OnDataChanged(object sender, TiledDataMessageArgs msg)
        {
            TiledMap.Change(msg.X, msg.Y, msg.Data);
        }

        private void OnClear(object sender, TiledClearMessageArgs msg)
        {
            TiledMap.Clear();
        }

        private void OnRefersh(object sender, TiledRefreshMessageArgs msg)
        {
            int x = msg.Data.GetLength(0);
            int y = msg.Data.GetLength(1);
            for(int i = 0; i < x; ++i)
            {
                for(int j = 0; j < y; ++j)
                {
                    TiledMap.Change(i, j, msg.Data[i, j]);
                }
            }
        }
    }
}
