using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerClearCommand : ICommand
    {
        private TiledMapDataContainer oriContainer;

        public void Do(TiledMapDataContainer container)
        {
            oriContainer = container.Clone() as TiledMapDataContainer;
            container.Clear();
        }

        public void Undo(TiledMapDataContainer container)
        {
            // 克隆并没有设置到tiledmap，这里先循环set一下
            //container = oriContainer.Clone() as TiledMapDataContainer;
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
