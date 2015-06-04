using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Command
{
    public interface ICommand
    {
        void Execute(TiledMapDataContainer container);
    }
}
