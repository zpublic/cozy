using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using CozyNote.Model.ObjectModel;
using Newtonsoft.Json;
using CozyNote.ServerCore.Database;
using CozyNote.Model.APIModel;

namespace CozyNote.ServerCore.Module
{
    public partial class NotebookModule
    {
        public string OnNotebookAll(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookAllInput>(args);
            var Result  = new NotebookAllOutput();

            if (DbHolding.User.IsExist(Input.UserName))
            {
                var User = DbHolding.User.Get(Input.UserName);
                if(User.pass == Input.UserPass)
                {
                    var NotebookList        = new List<Notebook>();
                    var UserNotebookList    = User.notebook_list;

                    foreach(var id in UserNotebookList)
                    {
                        if(DbHolding.Notebook.IsExist(id))
                        {
                            var notebook = DbHolding.Notebook.Get(id);
                            NotebookList.Add(notebook);
                        }
                    }

                    Result.ResultStatus = ResultStatus.SuccessStatus;
                    Result.NotebookList = NotebookList;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookGet(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookGetInput>(args);
            var Result  = new NotebookGetOutput();

            if(DbHolding.Notebook.IsExist(Input.NotebookId))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookId);

                if(notebook.pass == Input.NotebookPass)
                {
                    Result.ResultStatus = ResultStatus.SuccessStatus;
                    Result.NotebookName = notebook.name;
                    Result.NoteSum      = notebook.notes_num;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookList(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookListInput>(args);
            var Result  = new NotebookListOutput();
            if (DbHolding.Notebook.IsExist(Input.NotebookId))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookId);
                if (notebook.pass == Input.NotebookPass)
                {
                    var NoteList            = new List<Note>();
                    var NotebookNoteList    = notebook.note_list;

                    foreach(var id in NotebookNoteList)
                    {
                        if(DbHolding.Note.IsExist(id))
                        {
                            var note = DbHolding.Note.Get(id);
                            if(note.notebook_id == id)
                            {
                                NoteList.Add(note);
                            }
                        }
                    }

                    Result.ResultStatus = ResultStatus.SuccessStatus;
                    Result.NoteList     = NoteList;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookUpdate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookUpdateInput>(args);
            var Result  = new NotebookUpdateOutput();

            if (DbHolding.Notebook.IsExist(Input.NotebookId))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookId);

                if (notebook.pass == Input.NotebookPass)
                {
                    notebook.name = Input.NewName;
                    notebook.pass = Input.NewPass;
                }

                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookCreate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookCreateInput>(args);
            var Result  = new NotebookCreateOutput();

            if(DbHolding.User.IsExist(Input.UserName))
            {
                var NewNotebook = new Notebook()
                {
                    name = Input.NotebookName,
                    pass = Input.NotebookPass,
                };

                int id      = DbHolding.Notebook.Create(NewNotebook);
                var user    = DbHolding.User.Get(Input.UserName);
                user.notebook_list.Add(id);

                Result.NotebookId   = id;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookDelete(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookDeleteInput>(args);
            var Result  = new NotebookDeleteOutput();

            if (DbHolding.Notebook.IsExist(Input.NotebookId))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookId);

                if (notebook.pass == Input.NotebookPass)
                {
                    DbHolding.User.DeleteNotebook(Input.NotebookId);
                    DbHolding.Notebook.Delete(Input.NotebookId);

                    Result.ResultStatus = ResultStatus.SuccessStatus;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}
