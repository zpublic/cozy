using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using CozyNote.Model.ObjectModel;
using Newtonsoft.Json;
using CozyNote.Database;
using CozyNote.Model.APIModel;

namespace CozyNote.ServerCore.Module
{
    public partial class NotebookModule
    {
        public string OnNotebookAll(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookAllInput>(args);
            var Result  = new NotebookAllOutput();

            var user = ModuleHelper.GetUser(Input.UserName, Input.UserPass);
            if(user != null)
            {
                Result.NotebookList = user.notebook_list;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookGet(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookGetInput>(args);
            var Result  = new NotebookGetOutput();

            var notebook = ModuleHelper.GetNotebook(Input.NotebookId, Input.NotebookPass);
            if(notebook != null)
            {
                Result.NoteSum      = notebook.note_list.Count;
                Result.NotebookName = notebook.name;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookList(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookListInput>(args);
            var Result  = new NotebookListOutput();

            var notebook = ModuleHelper.GetNotebook(Input.NotebookId, Input.NotebookPass);
            if (notebook != null)
            {
                Result.NoteList     = notebook.note_list;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookUpdate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookUpdateInput>(args);
            var Result  = new NotebookUpdateOutput();

            var notebook = ModuleHelper.GetNotebook(Input.NotebookId, Input.NotebookPass);
            if (notebook != null)
            {
                notebook.name       = Input.NewName;
                notebook.pass       = Input.NewPass;
                DbHolding.Notebook.Update(notebook);
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNotebookCreate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NotebookCreateInput>(args);
            var Result  = new NotebookCreateOutput();

            var user = ModuleHelper.GetUser(Input.UserName, Input.UserPass);
            if(user != null)
            {
                var NewNotebook = new Notebook()
                {
                    name = Input.NotebookName,
                    pass = Input.NotebookPass,
                };
                NewNotebook.user_list.Add(user.id);
                int id = DbHolding.Notebook.Create(NewNotebook);

                user.notebook_list.Add(id);
                DbHolding.User.Update(user);

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

            var notebook = ModuleHelper.GetNotebook(Input.NotebookId, Input.NotebookPass);
            if(notebook != null)
            {
                foreach (var obj in notebook.user_list)
                {
                    var user = DbHolding.User.Get(obj);
                    user.notebook_list.Remove(notebook.id);
                    DbHolding.User.Update(user);
                }

                DbHolding.Notebook.Delete(Input.NotebookId);

                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}
