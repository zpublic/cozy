using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;

namespace CozyKxlol.MapEditor
{
    class TiledMapDataContainer
    {
        // get set
        // LoadMap
        // SaveMap

        public CozyTiledData TiledData { get; set; }

        public void Write(int x, int y, uint data)
        {
            TiledData.Change(x, y, data);
            DataChangedMessage(TiledData, new DataChangedMessageArgs(x, y, data));
        }

        public uint Read(int x, int y)
        {
            return TiledData[x, y];
        }


        public void LoadMap()
        {

        }

        public void SaveMap()
        {

        }

        public class DataChangedMessageArgs : EventArgs
        {
            public int X { get; set; }
            public int Y { get; set; }
            public uint Data { get; set; }

            public DataChangedMessageArgs(int x, int y, uint data)
            {
                X       = x;
                Y       = y;
                Data    = data;
            }
        }
        public event EventHandler<DataChangedMessageArgs> DataChangedMessage;
    }
}
