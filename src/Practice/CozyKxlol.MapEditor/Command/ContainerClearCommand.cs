using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerClearCommand : ICommand
    {
        public void Execute(TiledMapDataContainer container)
        {
            container.Clear();
        }
    }
}
