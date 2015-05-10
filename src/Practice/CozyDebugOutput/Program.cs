using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDebugOutput
{
    public static class Program
    {
        static void Main()
        {
            using (DbgView dbgView = new DbgView(Console.Out))
            {
                dbgView.Start();
                Console.ReadLine();
            }
        }
    }
}
