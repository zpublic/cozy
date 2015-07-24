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

        private uint oriData = 0;

        public ContainerModifyOne(int x, int y, uint data)
        {
            X       = x;
            Y       = y;
            Data    = data;
        }

        public void Do(TiledMapDataContainer container)
        {
            oriData = container.Read(X, Y);
            container.Write(X, Y, Data);
        }

        public void Undo(TiledMapDataContainer container)
        {
            container.Write(X, Y, oriData);
        }
    }
}
