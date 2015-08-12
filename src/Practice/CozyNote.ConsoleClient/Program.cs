using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;
using CozyNote.ConsoleClient.Scene;

namespace CozyNote.ConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program app = new Program();
            app.Run();
        }

        public void Run()
        {
            SceneManager.Instance.PushScene(new WelcomeScene());
            MainLoop();
        }

        public void MainLoop()
        {
            var scenes = SceneManager.Instance;
            while (!scenes.Empty)
            {
                var scene = SceneManager.Instance.CurrScene;
                scene.Run();
            }
        }
    }
}
