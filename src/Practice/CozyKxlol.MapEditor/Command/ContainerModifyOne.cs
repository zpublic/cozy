using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerModifyOne : ICommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Data { get; set; }

        public ContainerModifyOne(int x, int y, uint data)
        {
            X       = x;
            Y       = y;
            Data    = data;
        }
        public void Execute(TiledMapDataContainer container)
        {
            container.Write(X, Y, Data);
        }
    }
}
