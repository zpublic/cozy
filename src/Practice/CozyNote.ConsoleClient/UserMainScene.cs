using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ConsoleClient
{
    public class UserMainScene : IScene
    {
        private string Username;
        private string Password;
        private List<int> NotebookList;

        public UserMainScene(string username, string password, List<int> notebookList)
        {
            Username        = username;
            Password        = password;
            NotebookList    = notebookList;
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
            SceneManager.Instance.PushScene(new NotebookManagerScene(Username, Password, NotebookList));
        }

        private void OnModifyUserInfo()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {

        }
    }
}
