using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerSaveCommand : ICommand
    {
        private TiledMapDataContainer oriContainer;

        public void Do(TiledMapDataContainer container)
        {
            oriContainer = container.Clone() as TiledMapDataContainer;
            container.SaveMap();
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
