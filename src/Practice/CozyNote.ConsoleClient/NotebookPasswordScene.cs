using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient
{
    public class NotebookPasswordScene : IScene
    {
        private string Username { get; set; }

        private string Password { get; set; }

        private int Id { get; set; }

        public NotebookPasswordScene(string username, string password, int id)
        {
            Username = username;
            Password = password;
            Id = id;
        }

        public void Enter()
        {
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine("0.返回上层");
            Console.WriteLine("1.输入密码以查看Notebook");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        OnReturn();
                        break;
                    case 1:
                        OnEnterPass();
                        break;
                    default:
                        Console.WriteLine("指令错误");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void OnEnterPass()
        {
            Console.WriteLine("请输入Notebook密码");

            string pass = Console.ReadLine();
            List<int> Notes = null;
            if (NotebookApi.NotebookList(Id, pass, ref Notes))
            {
                SceneManager.Instance.PushScene(new NotebookScene(Username, Password, Id, pass));
                Console.WriteLine("密码正确 按下任意按键转入Notebook页面");
            }
            else
            {
                Console.WriteLine("密码错误");
            }
            Console.ReadKey();
        }

        private void OnReturn()
        {
            SceneManager.Instance.PopScene();
        }

        public void Exit()
        {
        }
    }
}
