using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.MapEditor.Command;

namespace CozyKxlol.MapEditor.Event
{
    public class TiledCommandArgs : EventArgs
    {
        public ICommand Command { get; set; }

        public TiledCommandArgs(ICommand command)
        {
            Command = command;
        }
    }
}
