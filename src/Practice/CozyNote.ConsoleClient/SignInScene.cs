using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient
{
    public class SignInScene : IScene
    {
        public void Enter()
        {
            
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine("0.返回上层");
            Console.WriteLine("1.登陆");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        SceneManager.Instance.PopScene();
                        break;
                    case 1:
                        OnSignIn();
                        break;
                    default:
                        Console.WriteLine("指令错误");
                        break;
                }
            }
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

            }
            else
            {
                Console.WriteLine("用户名或密码错误"); ;
            }
            Console.WriteLine("按下任意键继续");
            Console.ReadKey();
        }

        public void Exit()
        {

        }
    }
}
