using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.MapEditor.Command;
using CozyKxlol.MapEditor.Event;

namespace CozyKxlol.MapEditor.Gui.OperateLayer
{
    public partial class MapEditorSceneOperateLayer
    {
        private void OnLoad()
        {
            var command = new ContainerLoadCommand();
            TiledCommandMessages(this, new TiledCommandArgs(command));
        }

        private void OnSave()
        {
            var command = new ContainerSaveCommand();
            TiledCommandMessages(this, new TiledCommandArgs(command));
        }

        private void OnClear()
        {
            var command = new ContainerClearCommand();
            TiledCommandMessages(this, new TiledCommandArgs(command));
        }
    }
}
