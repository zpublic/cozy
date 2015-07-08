using CozyAnywhere.Plugin.WinKeyboard;
using System;

namespace ConsoleKeyboardTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while(true)
            {
                if(KeyboardUtil.QueryKeyState(VirtualKey.VK_SPACE))
                {
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_H);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_E);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_L);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_L);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_O);
                    KeyboardUtil.SendKeyEvent(VirtualKey.VK_SPACE);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_W);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_O);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_R);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_L);
                    KeyboardUtil.SendKeyEvent(VirtualKey.K_D);
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}