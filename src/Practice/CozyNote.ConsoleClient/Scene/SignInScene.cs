using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient.Scene
{
    public class SignInScene : SceneBase
    {
        private Menu menu { get; set; }

        public override void Enter()
        {
            menu = new Menu();
            menu.Add(new MenuItem() { Text = "返回", Command = OnReturn, });
            menu.Add(new MenuItem() { Text = "登陆", Command = OnSignIn, });
        }

        public override void Run()
        {
            Console.Clear();
            Menu.Print(menu);
            menu.Input();
        }

        private void OnReturn()
        {
            SceneManager.Instance.PopScene();
        }

        private void OnSignIn()
        {
            Console.WriteLine("请输入用户名:");
            string username = Console.ReadLine();
            Console.WriteLine("请输入用户密码:");
            string password = Console.ReadLine();

            List<int> notebooklist = null;
            if(UserApi.UserNotebook(username, password, ref notebooklist))
            {
                Console.WriteLine("登陆成功 按下任意按键转到用户界面");
                SceneManager.Instance.PushScene(new UserMainScene(username, password, notebooklist));
            }
            else
            {
                Console.WriteLine("用户名或密码错误"); ;
            }
            Console.ReadKey();
        }
    }
}
