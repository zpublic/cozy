using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient.Scene
{
    public class SignUpScene : IScene
    {
        public void Enter()
        {
            
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine("0.返回上层");
            Console.WriteLine("1.注册新用户");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        OnReturn();
                        break;
                    case 1:
                        OnSignUp();
                        break;
                    default:
                        Console.WriteLine("指令错误");
                        break;
                }
            }
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

        public void Exit()
        {

        }
    }
}
