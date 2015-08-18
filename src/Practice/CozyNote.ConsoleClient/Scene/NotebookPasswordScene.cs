using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient.Scene
{
    public class NotebookPasswordScene : SceneBase
    {
        private string Username { get; set; }

        private string Password { get; set; }

        private int Id { get; set; }

        private Menu menu { get; set; }

        public NotebookPasswordScene(string username, string password, int id)
        {
            Username    = username;
            Password    = password;
            Id          = id;
        }

        public override void Enter()
        {
            menu = new Menu();
            menu.Add(new MenuItem() { Text = "返回", Command = OnReturn, });
            menu.Add(new MenuItem() { Text = "输入密码", Command = OnEnterPass, });

        }

        public override void Run()
        {
            Console.Clear();
            Menu.Print(menu);
            menu.Input();
        }

        private void OnEnterPass()
        {
            Console.WriteLine("请输入Notebook密码");

            string pass     = Console.ReadLine();
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
    }
}
