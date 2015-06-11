using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Engine.Tiled.Impl;
using Microsoft.Xna.Framework;
using CozyKxlol.MapEditor.Command;
using CozyKxlol.MapEditor.Event;

namespace CozyKxlol.MapEditor
{
    public class TiledMapDataContainer : ICloneable
    {
        // get set
        // LoadMap
        // SaveMap

        public CozyTiledData TiledData { get; set; }
        public Point MapSize { get; set; }

        private string DataPath = @"Data.db";

        public void Write(int x, int y, uint data)
        {
            TiledData.Modify(x, y, data);
            DataMessage(TiledData, new TiledDataMessageArgs(x, y, data));
        }

        public uint Read(int x, int y)
        {
            return TiledData[x, y];
        }

        public TiledMapDataContainer(int x, int y)
        {
            TiledData   = new CozyTiledData(x, y);
            MapSize     = new Point(x, y);
        }

        public event EventHandler<TiledDataMessageArgs> DataMessage;
        public event EventHandler<TiledClearMessageArgs> ClearMessage;

        public void LoadMap()
        {
            var loader = new CozyTiledDataLoader(DataPath);
            loader.Load(TiledData);
        }

        public void SaveMap()
        {
            var writer = new CozyTiledDataWriter(DataPath);
            writer.Write(TiledData);
        }

        public void Clear()
        {
            ClearMessage(TiledData, new TiledClearMessageArgs());
        }

        public object Clone()
        {
            return this.MemberwiseClone(); 
        }
    }
}
