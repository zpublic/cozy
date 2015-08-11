using System;
using CozyNote.Model.ObjectModel;
using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CozyNote.ServerTester
{
    public partial class Program
    {
        public static int CreateUser(string username, string pass)
        {
            var usercreateinput = new UserCreateInput()
            {
                UserName = username,
                UserPass = pass,
            };
            var output              = usermodule.OnUserCreate(JsonConvert.SerializeObject(usercreateinput));
            var usercreateoutput    = JsonConvert.DeserializeObject<UserCreateOutput>(output);
            return usercreateoutput.UserId;
        }

        public static int CreateNotebook(string username, string userpass, string notebookname, string notebookpass)
        {
            var notebookcreateinput = new NotebookCreateInput()
            {
                UserName        = username,
                UserPass        = userpass,
                NotebookName    = notebookname,
                NotebookPass    = notebookpass,
            };
            var output                  = notebookmodule.OnNotebookCreate(JsonConvert.SerializeObject(notebookcreateinput));
            var notebookcreateoutput    = JsonConvert.DeserializeObject<NotebookCreateOutput>(output);
            return notebookcreateoutput.NotebookId;
        }

        public static int CreateNote(int notebookid, string notebookpass, string notename, int type, string notedate)
        {
            var notecreateinput = new NoteCreateInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
                NoteName        = notename,
                NoteType        = type,
                NoteData        = notedate,
            };
            var output              = notemodule.OnNoteCreate(JsonConvert.SerializeObject(notecreateinput));
            var usercreateoutput    = JsonConvert.DeserializeObject<NoteCreateOutput>(output);
            return usercreateoutput.NoteId;
        }

        public static List<int> GetUserNotebook(string username, string userpass)
        {
            var usernotebookinput = new UserNotebookInput()
            {
                UserName = username,
                UserPass = userpass,
            };
            var output              = usermodule.OnUserNotebook(JsonConvert.SerializeObject(usernotebookinput));
            var usernotebookoutput  = JsonConvert.DeserializeObject<UserNotebookOutput>(output);
            return usernotebookoutput.NotebookList;
        }

        public static Tuple<string, int> GetNotebook(int notebookid, string notebookpass)
        {
            var notebookgetinput = new NotebookGetInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
            };
            var output              = notebookmodule.OnNotebookGet(JsonConvert.SerializeObject(notebookgetinput));
            var notebookgetoutput   = JsonConvert.DeserializeObject<NotebookGetOutput>(output);
            return Tuple.Create(notebookgetoutput.NotebookName, notebookgetoutput.NoteSum);
        }

        public static List<int> GetNotebookNoteList(int notebookid, string notebookpass)
        {
            var notebooklistinput = new NotebookListInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
            };
            var output              = notebookmodule.OnNotebookList(JsonConvert.SerializeObject(notebooklistinput));
            var notebooklistoutput  = JsonConvert.DeserializeObject<NotebookListOutput>(output);
            return notebooklistoutput.NoteList;
        }

        public static Note GetNote(int notebookid, string notebookpass, int noteid)
        {
            var notegetinput = new NoteGetInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
                NoteId          = noteid,
            };
            var output          = notemodule.OnNoteGet(JsonConvert.SerializeObject(notegetinput));
            var notegetoutput   = JsonConvert.DeserializeObject<NoteGetOutput>(output);
            return notegetoutput.Result;
        }
    }
}
