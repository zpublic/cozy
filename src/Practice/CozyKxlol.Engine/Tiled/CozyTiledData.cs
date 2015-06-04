using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledData
    {
        private uint[,] Data { get; set; }

        public CozyTiledData(int x, int y)
        {
            Data = new uint[x, y];
        }

        public uint this[int x, int y]
        {
            get
            {
                return Data[x, y];
            }
        }

        public void Change(int x, int y, uint data)
        {
            Data[x, y] = data;
        }

        public void Remove(int x, int y)
        {
            Data[x, y] = 0;
        }
    }
}
