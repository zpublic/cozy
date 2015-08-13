using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient.Scene
{
    public class SignUpScene : SceneBase
    {
        private Menu menu { get; set; }

        public override void Enter()
        {
            menu = new Menu();
            menu.Add(new MenuItem() { Text = "返回", Command = OnReturn, });
            menu.Add(new MenuItem() { Text = "注册", Command = OnSignUp, });
        }

        public override void Run()
        {
            Console.Clear();
            Menu.Print(menu);
            menu.Input();
        }

        private void OnSignUp()
        {
            Console.WriteLine("请输入新用户名:");
            string username = Console.ReadLine();
            Console.WriteLine("请输入新用户密码:");
            string password = Console.ReadLine();

            int userid = 0;
            if(UserApi.UserCreate(username, password, ref userid))
            {
                Console.WriteLine("注册成功 用户id : {0}", userid);
                Console.WriteLine("按任意键返回上层");
                SceneManager.Instance.PopScene();
            }
            else
            {
                Console.WriteLine("注册失败");
            }
            Console.ReadKey();
        }

        private void OnReturn()
        {
            SceneManager.Instance.PopScene();
        }
    }
}
