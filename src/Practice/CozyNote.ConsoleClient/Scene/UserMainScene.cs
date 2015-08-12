using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient.Scene
{
    public class UserMainScene : IScene
    {
        private string Username;
        private string Password;
        private List<int> NotebookList;

        public UserMainScene(string username, string password, List<int> notebookList)
        {
            Username = username;
            Password = password;
            NotebookList = notebookList;
        }

        public void Enter()
        {

        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine("0.返回上层");
            Console.WriteLine("1.管理Notebook");
            Console.WriteLine("2.修改个人信息");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        OnReturn();
                        break;
                    case 1:
                        OnModifyNotebook();
                        break;
                    case 2:
                        OnModifyUserInfo();
                        break;
                    default:
                        Console.WriteLine("指令错误");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void OnReturn()
        {
            SceneManager.Instance.PopScene();
        }

        private void OnModifyNotebook()
        {
            SceneManager.Instance.PushScene(new NotebookManagerScene(Username, Password));
        }

        private void OnModifyUserInfo()
        {
            Console.WriteLine("输入新名称");
            string newName = Console.ReadLine();
            Console.WriteLine("输入新密码");
            string newPass = Console.ReadLine();

            if (UserApi.UserUpdate(Username, Password, newName, newPass))
            {
                Console.WriteLine("修改成功 按下任意按键返回重新登录");
                SceneManager.Instance.PopScene();
            }
            else
            {
                Console.WriteLine("修改失败");
            }
            Console.ReadKey();
        }

        public void Exit()
        {

        }
    }
}
