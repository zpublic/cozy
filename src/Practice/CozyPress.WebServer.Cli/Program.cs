using CozyPress.WebServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPress.WebServer.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyPressServer server = new CozyPressServer();
            server.Run("http://localhost:12306");
            Console.ReadKey();
        }
    }
}
