using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ConsoleClient
{
    public interface IScene
    {
        void Enter();

        void Run();

        void Exit();
    }
}
