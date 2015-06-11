using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledData : ICozyLoader, ICozyWriter
    {
        private uint[,] Data { get; set; }

        public Point DataSize { get; private set; }

        public CozyTiledData(int x, int y)
        {
            DataSize    = new Point(x, y);
            Data        = new uint[x, y];
        }

        public uint this[int x, int y]
        {
            get
            {
                if(Judge(x, y))
                {
                    return Data[x, y];
                }
                return 0;
            }
        }

        private bool Judge(int x, int y)
        {
            return (x >= 0 && x < DataSize.X && y >= 0 && y < DataSize.Y);
        }

        public void Modify(int x, int y, uint data)
        {
            if (Judge(x, y))
            {
                Data[x, y] = data;
            }
        }

        public void Remove(int x, int y)
        {
            if (Judge(x, y))
            {
                Data[x, y] = 0;
            }
        }

        public void Clear()
        {
            for(int i = 0; i < DataSize.X; ++i)
            {
                for(int j = 0; j < DataSize.Y; ++j)
                {
                    Data[i, j] = 0;
                }
            }
        }

        public void Load(Stream LoadStream)
        {
            if (LoadStream != null && Data != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Data = (uint[,])formatter.Deserialize(LoadStream);
            }
        }

        public void Write(Stream WriteStream)
        {
            if (WriteStream != null && Data != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(WriteStream, Data);
            }
        }
    }
}
