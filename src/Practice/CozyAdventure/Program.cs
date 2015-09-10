using CocosSharp;
using System;

namespace CozyAdventure
{
    public static class Program
    {
        static void Main()
        {
            CCApplication application = new CCApplication(false, new CCSize(800f, 450f));
            application.ApplicationDelegate = new AppDelegate();
            application.StartGame();
        }
    }
}
