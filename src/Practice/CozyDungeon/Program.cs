using CocosSharp;
using System;

namespace CozyDungeon
{
    public static class Program
    {
        static void Main()
        {
            CCApplication application = new CCApplication(false, new CCSize(1024f, 768f));
            application.ApplicationDelegate = new AppDelegate();
            application.StartGame();
        }
    }
}
