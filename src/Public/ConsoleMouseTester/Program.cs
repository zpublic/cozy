using CozyAnywhere.Plugin.WinMouse;
using System;
using System.Threading;

namespace ConsoleMouseTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (int i = 0; i < 180; ++i)
            {
                int x = 100 + (int)(100 * Math.Sin(i / Math.PI));
                int y = 100 + (int)(100 * Math.Cos(i / Math.PI));
                MouseUtil.SetCursorPosition(x, y);
                Thread.Sleep(10);
            }

            for (int i = 0; i < 5; ++i)
            {
                int x = 0;
                int y = 0;
                MouseUtil.GetCursorPosition(ref x, ref y);
                MouseUtil.LeftClick(0, 0);
                Console.WriteLine("Click at ( X: {0}, Y: {1} )", x, y);
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
    }
}