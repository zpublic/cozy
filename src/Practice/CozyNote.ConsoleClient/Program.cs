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

        private void TestDate()
        {
            Console.WriteLine("before create notebook");
            int uid = 0;
            UserApi.UserCreate("kingwl", "123456", ref uid);

            List<int> list = null;
            if (UserApi.UserNotebook("kingwl", "123456", ref list))
            {
                Console.WriteLine("user notebook list : ");
                foreach (var obj in list)
                {
                    Console.WriteLine(obj);
                }
            }

            Console.WriteLine("after create notebook");
            int noteid = 0;
            NotebookApi.NotebookCreate("kingwl", "123456", "test", "654321", ref noteid);

            if (UserApi.UserNotebook("kingwl", "123456", ref list))
            {
                Console.WriteLine("user notebook list : ");
                foreach (var obj in list)
                {
                    Console.WriteLine(obj);
                }
            }

            Console.WriteLine("after delete notebook");
            NotebookApi.NotebookDelete(noteid, "654321");

            if (UserApi.UserNotebook("kingwl", "123456", ref list))
            {
                Console.WriteLine("user notebook list : ");
                foreach (var obj in list)
                {
                    Console.WriteLine(obj);
                }
            }

            Console.WriteLine("hello world!");
            Console.ReadKey();
        }
    }
}
