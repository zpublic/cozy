using CozyApiPlayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyApiPlayer.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiPlayerEngine player = new ApiPlayerEngine();
            player.Init(@"c:\1.json");
            player.RunProject();
        }
    }
}
