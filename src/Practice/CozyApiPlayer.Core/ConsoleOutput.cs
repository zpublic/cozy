using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyApiPlayer.Core
{
    class ConsoleOutput : IResultOutput
    {
        public void onResult(string s)
        {
            Console.WriteLine(s);
        }
    }
}
