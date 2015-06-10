using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Event
{
    public class DataMessageArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Data { get; set; }

        public DataMessageArgs(int x, int y, uint data)
        {
            X = x;
            Y = y;
            Data = data;
        }
    }
}
