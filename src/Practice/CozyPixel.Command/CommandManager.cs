using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Command
{
    public class CommandManager
    {
        private static CommandManager instance { get; set; } = new CommandManager();

        public static CommandManager Instance { get { return instance; } }

        private Stack<IPixelCommand> UndoList { get; set; } = new Stack<IPixelCommand>();
        private Stack<IPixelCommand> RedoList { get; set; } = new Stack<IPixelCommand>();

        public void Do(IPixelCommand command)
        {
            if(RedoList.Count > 0)
            {
                RedoList.Clear();
            }

            command.Do();
            UndoList.Push(command);
        }

        public void Undo()
        {
            if(UndoList.Count > 0)
            {
                var command = UndoList.Pop();
                command.Undo();
                RedoList.Push(command);
            }
        }

        public void Redo()
        {
            if(RedoList.Count > 0)
            {
                var command = RedoList.Pop();
                command.Do();
                UndoList.Push(command);
            }
        }
    }
}
