using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;

namespace CozyKxlol.MapEditor
{
    public class MapEditorSceneTiledLayer : CozyLayer
    {
        // update and show current map from TiledMapDataContainer`s data
        public CozyTiledMap TiledMap { get; set; }

        public MapEditorSceneTiledLayer()
        {
            var size = MapEditorSceneLayer.Container.MapSize;
            TiledMap = new CozyTiledMap(size);
            TiledMap.Factory.Create = OnCreate;

            MapEditorSceneLayer.Container.DataChangedMessage += OnDataChanged;
            this.AddChind(TiledMap);
        }

        private void OnDataChanged(object sender, 
            TiledMapDataContainer.DataChangedMessageArgs msg)
        {
            TiledMap.Change(msg.X, msg.Y, msg.Data);
        }

        public CozyTiledNode OnCreate(uint id)
        {
            CozyTiledNode node = null;
            switch(id)
            {
                case 1:
                    node = new CozyGreenTiled();
                    break;

                default:
                    node = new CozyDefaultTiled();
                    break;
            }
            return node;
        }
    }
}
