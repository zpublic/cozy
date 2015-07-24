using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerLoadCommand : ICommand
    {
        private TiledMapDataContainer oriContainer;
        public void Do(TiledMapDataContainer container)
        {
            oriContainer = container.Clone() as TiledMapDataContainer;
            container.LoadMap();
        }

        public void Undo(TiledMapDataContainer container)
        {
            for (int x = 0; x < container.MapSize.X; ++x)
            {
                for (int y = 0; y < container.MapSize.Y; ++y)
                {
                    container.Write(x, y, oriContainer.Read(x, y));
                }
            }
        }
    }
}
