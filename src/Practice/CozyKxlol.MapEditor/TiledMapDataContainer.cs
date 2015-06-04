using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;
using CozyKxlol.MapEditor.Command;

namespace CozyKxlol.MapEditor
{
    public class TiledMapDataContainer
    {
        // get set
        // LoadMap
        // SaveMap

        public CozyTiledData TiledData { get; set; }
        public Point MapSize { get; set; }

        public void Write(int x, int y, uint data)
        {
            TiledData.Change(x, y, data);
            DataChangedMessage(TiledData, new DataChangedMessageArgs(x, y, data));
        }

        public uint Read(int x, int y)
        {
            return TiledData[x, y];
        }

        public TiledMapDataContainer(int x, int y)
        {
            TiledData = new CozyTiledData(x, y);
            MapSize = new Point(x, y);
        }


        public void InvokeCommand(ICommand command)
        {
            command.Execute(this);
        }

        #region DataMessage to TiledLayer
        public class DataChangedMessageArgs : EventArgs
        {
            public int X { get; set; }
            public int Y { get; set; }
            public uint Data { get; set; }

            public DataChangedMessageArgs(int x, int y, uint data)
            {
                X = x;
                Y = y;
                Data = data;
            }
        }
        public event EventHandler<DataChangedMessageArgs> DataChangedMessage;
        #endregion


        public void LoadMap()
        {

        }

        public void SaveMap()
        {

        }
    }
}
