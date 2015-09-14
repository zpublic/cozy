using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyServer.Core;

using ServerType = CozyServer.Core.CozyServer;

namespace CozyServer.Console
{
    public class Program
    {
        static ServerType server { get; set; }
        static void Main(string[] args)
        {
            server = new ServerType("CozyAdventure", 1000, 44360);
            server.Listen();
            server.EnterMainLoop();
        }
    }
}
