using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;
using CozyKxlol.MapEditor.Command;
using CozyKxlol.MapEditor.Event;

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
            DataMessage(TiledData, new DataMessageArgs(x, y, data));
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


        public void InvokeCommand(ICommand command)
        {
            command.Execute(this);
        }

        public event EventHandler<DataMessageArgs> DataMessage;

        public void LoadMap()
        {

        }

        public void SaveMap()
        {

        }

        public void Clear()
        {
            for(int i = 0; i < MapSize.X; ++i)
            {
                for (int j = 0; j < MapSize.Y; ++j)
                {
                    TiledData.Remove(i, j);
                    DataMessage(TiledData, new DataMessageArgs(i, j, 0));
                }
            }
        }
    }
}
