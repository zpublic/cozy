using System;
using CozyNote.ServerCore.Module;
using System.IO;

namespace CozyNote.ServerTester
{
    public partial class Program
    {
        const string TestUserName       = "kingwl";
        const string TestUserPass       = "123456";
        const string TestNotebookName   = "school days";
        const string TestNotebookPass   = "654321";
        const string TestNoteName       = "oneday";
        const string TestNoteDate       = "ahahahahahaha";

        public static NotebookModule notebookmodule = new NotebookModule();

        public static NoteModule notemodule         = new NoteModule();

        public static UserModule usermodule         = new UserModule();

        static void Main(string[] args)
        {
            Clear();
            InitTestDate();
            RunTest();
            Console.ReadKey();
        }

        public static void Clear()
        {
            if (File.Exists(@"notebook.db"))
            {
                File.Delete(@"notebook.db");
            }
            if (File.Exists(@"note.db"))
            {
                File.Delete(@"note.db");
            }
            if (File.Exists(@"user.db"))
            {
                File.Delete(@"user.db");
            }

            Console.WriteLine("Clear Complete");
        }

        public static void InitTestDate()
        {
            int userid      = CreateUser(TestUserName, TestUserPass);

            int notebookid  = CreateNotebook(TestUserName, TestUserPass, TestNotebookName, TestNotebookPass);

            int noteid      = CreateNote(notebookid, TestNotebookPass, TestNoteName, 0, TestNoteDate);

            Console.WriteLine("Init Complete");
            Console.WriteLine("UserId : {0} NotebookId : {1} NoteId : {2}", userid, notebookid, noteid);
        }

        public static void RunTest()
        {
            var usernotebooklist = GetUserNotebook(TestUserName, TestUserPass);
            Console.WriteLine("UserNotebookIdList :");
            foreach(var notebookid in usernotebooklist)
            {
                var notebook = GetNotebook(notebookid, TestNotebookPass);
                Console.WriteLine("Id : {0}", notebookid);
                Console.WriteLine("Name : {0}", notebook.Item1);
                Console.WriteLine("Count : {0}", notebook.Item2);

                var notebooknotelist = GetNotebookNoteList(notebookid, TestNotebookPass);
                Console.WriteLine("NotebookNoteList : ");
                foreach(var noteid in notebooknotelist)
                {
                    var note = GetNote(notebookid, TestNotebookPass, noteid);
                    Console.WriteLine("NoteName : {0}, NoteType : {1}, NoteDate : {2}", note.name, note.type, note.data);
                }
            }
        }
    }
}
