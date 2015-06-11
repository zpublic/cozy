using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Command
{
    public class CommandHistory
    {
        private static CommandHistory s_instance = new CommandHistory();
        private Stack<ICommand> m_undoCommandStack = new Stack<ICommand>();
        private Stack<ICommand> m_redoCommandStack = new Stack<ICommand>();

        public static CommandHistory Instance
        {
            get { return s_instance; }
        }

        public bool CanUndo()
        {
            return m_undoCommandStack.Count > 0;
        }

        public bool CanRedo()
        {
            return m_redoCommandStack.Count > 0;
        }

        public void Clear()
        {
            m_undoCommandStack.Clear();
            m_redoCommandStack.Clear();
        }

        public void Do(ICommand command, TiledMapDataContainer container)
        {
            command.Do(container);
            m_undoCommandStack.Push(command);
            m_redoCommandStack.Clear();
        }

        public void Undo(TiledMapDataContainer container)
        {
            if (m_undoCommandStack.Count == 0)
                throw new Exception("No commands to undo");

            ICommand command = m_undoCommandStack.Pop();
            command.Undo(container);
            m_redoCommandStack.Push(command);
        }

        public void Redo(TiledMapDataContainer container)
        {
            if (m_redoCommandStack.Count == 0)
                throw new Exception("No commands to redo");

            ICommand command = m_redoCommandStack.Pop();
            command.Do(container);
            m_undoCommandStack.Push(command);
        }

        public int Count
        {
            get { return m_undoCommandStack.Count + m_redoCommandStack.Count; }
        }
    }
}
