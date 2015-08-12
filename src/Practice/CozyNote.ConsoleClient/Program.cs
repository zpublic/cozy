using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

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
            TestDate();
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
