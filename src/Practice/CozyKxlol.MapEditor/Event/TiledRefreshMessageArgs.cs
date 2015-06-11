using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Event
{
    public class TiledRefreshMessageArgs : EventArgs
    {
        public uint[,] Data { get; set; }

        public TiledRefreshMessageArgs(uint[,] data)
        {
            Data = data;
        }
    }
}
