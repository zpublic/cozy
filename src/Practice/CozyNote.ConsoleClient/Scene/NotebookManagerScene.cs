using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;

namespace CozyNote.ConsoleClient.Scene
{
    public class NotebookManagerScene : SceneBase
    {
        private string Username { get; set; }

        private string Password { get; set; }

        private List<int> NotebookList { get; set; }

        private Menu menu { get; set; }

        public NotebookManagerScene(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override void Enter()
        {
            menu = new Menu();
            menu.Add(new MenuItem() { Text = "返回", Command = OnReturn, });
            menu.Add(new MenuItem() { Text = "查看所有Notebook", Command = OnEnumNotebook, });
            menu.Add(new MenuItem() { Text = "管理Notebook", Command = OnModifyNotebook, });
            menu.Add(new MenuItem() { Text = "创建Notebook", Command = OnCreateNotebook, });
            menu.Add(new MenuItem() { Text = "删除Notebook", Command = OnDeleteNotebook, });
        }

        public override void Run()
        {
            Console.Clear();
            Menu.Print(menu);
            menu.Input();
        }

        private void OnEnumNotebook()
        {
            List<int> notebooklist = null;
            if (UserApi.UserNotebook(Username, Password, ref notebooklist))
            {
                NotebookList = notebooklist;

                Console.WriteLine("您的Notebook有:");
                Tuple<string, int> result = null;
                foreach (var obj in NotebookList)
                {
                    Console.WriteLine("Id : {0}", obj);
                    if (NotebookApi.NotebookGet(obj, ref result))
                    {
                        Console.WriteLine("Name : {0}", result.Item1);
                        Console.WriteLine("NoteSum : {0}", result.Item2);
                    }
                }
            }
            else
            {
                Console.WriteLine("获取信息失败");
            }
            Console.ReadKey();
        }

        private void OnModifyNotebook()
        {
            Console.WriteLine("请输入NotebookId");
            int id = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out id) && NotebookList.Contains(id))
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
            Console.WriteLine("输入NotebookName");
            string notebookname = Console.ReadLine();
            Console.WriteLine("输入NotebookPass");
            string notebookpass = Console.ReadLine();

            int notebookid = 0;
            if (NotebookApi.NotebookCreate(Username, Password, notebookname, notebookpass, ref notebookid))
            {
                Console.WriteLine("创建成功");
            }
            else
            {
                Console.WriteLine("创建失败");
            }
            Console.ReadKey();
        }

        private void OnDeleteNotebook()
        {
            Console.WriteLine("输入要删除的Notebook的ID");
            int id = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out id))
            {
                Console.WriteLine("输入要删除的Notebook的密码");
                string pass = Console.ReadLine();
                if(NotebookApi.NotebookDelete(id, pass))
                {
                    Console.WriteLine("删除成功");
                }
                else
                {
                    Console.WriteLine("密码错误");
                }
            }
            else
            {
                Console.WriteLine("输入错误");
            }
            Console.ReadKey();
        }

        private void OnReturn()
        {
            SceneManager.Instance.PopScene();
        }
    }
}
