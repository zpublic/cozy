using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient
{
    public class NotebookManagerScene : IScene
    {
        private string Username { get; set; }

        private string Password { get; set; }

        private List<int> NotebookList { get; set; }

        public NotebookManagerScene(string username, string password, List<int> notebookList)
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
            Console.WriteLine("1.查看所有Notebook");
            Console.WriteLine("2.管理Notebook");
            Console.WriteLine("3.创建Notebook");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        OnReturn();
                        break;
                    case 1:
                        OnEnumNotebook();
                        break;
                    case 2:
                        OnModifyNotebook();
                        break;
                    case 3:
                        OnCreateNotebook();
                        break;
                    default:
                        Console.WriteLine("指令错误");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void OnEnumNotebook()
        {
            Console.WriteLine("您的Notebook有:");
            Tuple<string, int> result = null;
            foreach(var obj in NotebookList)
            {
                Console.WriteLine("Id : {0}", obj);
                if(NotebookApi.NotebookGet(obj, ref result))
                {
                    Console.WriteLine("Name : {0}", result.Item1);
                    Console.WriteLine("NoteSum : {0}", result.Item2);
                }
            }
            Console.ReadKey();
        }

        private void OnModifyNotebook()
        {
            Console.WriteLine("请输入NotebookId");
            int id = 0;
            if(int.TryParse(Console.ReadLine().Trim(), out id) && NotebookList.Contains(id))
            {
                SceneManager.Instance.PushScene(new NotebookPasswordScene(Username, Password, id));
            }
            else
            {
                Console.WriteLine("NotebookId 错误");
                Console.ReadKey();
            }
        }

        private void OnCreateNotebook()
        {
            throw new NotImplementedException();
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
