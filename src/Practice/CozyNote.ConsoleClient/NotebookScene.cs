﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.ClientCore.Api;
using CozyNote;

namespace CozyNote.ConsoleClient
{
    public class NotebookScene : IScene
    {
        private string Username { get; set; }

        private string Password { get; set; }

        private int NotebookId { get; set; }

        private string NotebookPass { get; set; }

        private List<int> NoteList { get; set; }

        public NotebookScene(string username, string password, int notebookid, string notebookpass)
        {
            Username = username;
            Password = password;
            NotebookId = notebookid;
            NotebookPass = notebookpass;
        }

        public void Enter()
        {

        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("欢迎使用CozyNote，您可以输入以下指令:");
            Console.WriteLine("0.返回上层");
            Console.WriteLine("1.查看所有Note");
            Console.WriteLine("2.创建Note");
            Console.WriteLine("3.删除Note");

            int n = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out n))
            {
                switch (n)
                {
                    case 0:
                        OnReturn();
                        break;
                    case 1:
                        OnEnumNote();
                        break;
                    case 2:
                        OnNoteCreate();
                        break;
                    case 3:
                        OnNoteDelete();
                        break;
                    default:
                        Console.WriteLine("指令错误");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void OnEnumNote()
        {
            List<int> notelist = null;
            if(NotebookApi.NotebookList(NotebookId, NotebookPass, ref notelist))
            {
                NoteList = notelist;

                Console.WriteLine("Id为{0}的Notebook中 有如下Note", NotebookId);
                foreach (var obj in NoteList)
                {
                    Console.WriteLine("Id : {0}", obj);
                    Tuple<int, string, string> note = null;
                    if (NoteApi.NoteGet(NotebookId, NotebookPass, obj, ref note))
                    {
                        Console.WriteLine("Type : {0}", note.Item1);
                        Console.WriteLine("Name : {0}", note.Item2);
                        Console.WriteLine("Data : {0}", note.Item3);
                    }
                    else
                    {
                        Console.WriteLine("获取失败");
                    }
                }
            }
            else
            {
                Console.WriteLine("获取数据失败");
            }
            
            Console.ReadKey();
        }

        private void OnReturn()
        {
            SceneManager.Instance.PopScene();
        }

        private void OnNoteCreate()
        {
            int type = 0;
            if (int.TryParse(Console.ReadLine().Trim(), out type))
            {
                string notename = Console.ReadLine();
                string notedata = Console.ReadLine();
                int noteid = 0;
                if (NoteApi.NoteCreate(NotebookId, NotebookPass, notename, type, notedata, ref noteid))
                {
                    Console.WriteLine("创建成功");
                }
                else
                {
                    Console.WriteLine("创建失败");
                }
            }
            else
            {
                Console.WriteLine("Type Error");
            }
            Console.ReadKey();
        }

        private void OnNoteDelete()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {

        }
    }
}
