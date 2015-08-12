using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient
{
    public class WelcomeScene : IScene
    {
        public void Enter()
        {
            
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine("1.注册用户");
            Console.WriteLine("2.登陆");
            Console.WriteLine("0.退出");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        OnExit();
                        return;
                    case 1:
                        OnSignUp();
                        return;
                    case 2:
                        OnSignIn();
                        return;
                    default:
                        Console.WriteLine("指令错误");
                        break;
                }
            }
            else
            {
                Console.WriteLine("输入错误");
            }
            Console.ReadKey();
        }

        private void OnSignIn()
        {
            SceneManager.Instance.PushScene(new SignInScene());
        }


        private void OnSignUp()
        {
            SceneManager.Instance.PushScene(new SignUpScene());
        }

        private void OnExit()
        {
            SceneManager.Instance.Exit();
        }

        public void Exit()
        {
        }
    }
}
