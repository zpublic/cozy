using CozyNote.ServerCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerHost.Run("http://localhost:23333");
            Console.ReadLine();
        }
    }
}
