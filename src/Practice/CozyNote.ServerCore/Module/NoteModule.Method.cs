using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.Model.APIModel;
using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using CozyNote.Database;
using Newtonsoft.Json;
using CozyNote.Model.ObjectModel;

namespace CozyNote.ServerCore.Module
{
    public partial class NoteModule
    {
        public string OnNoteCreate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteCreateInput>(args);
            var Result  = new NoteCreateOutput();

            if(DbHolding.Notebook.IsExist(Input.NotebookName))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookName);
                if(notebook.pass == Input.NotebookPass)
                {
                    var note = new Note()
                    {
                        notebook_id = notebook.id,
                        name        = Input.NoteName,
                        type        = Input.NoteType,
                        data        = Input.NoteData,
                    };

                    var id = DbHolding.Note.Create(note);
                    notebook.note_list.Add(id);
                    notebook.notes_num++;

                    Result.NoteId       = id;
                    Result.ResultStatus = ResultStatus.SuccessStatus;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteGet(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteGetInput>(args);
            var Result  = new NoteGetOutput();

            if(DbHolding.Notebook.IsExist(Input.NotebookName))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookName);
                if(notebook.pass == Input.NotebookPass)
                {
                    if(notebook.note_list.Contains(Input.NoteId) && DbHolding.Note.IsExist(Input.NoteId))
                    {
                        var note            = DbHolding.Note.Get(Input.NoteId);
                        Result.Result       = note;
                        Result.ResultStatus = ResultStatus.SuccessStatus;
                    }
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteUpdate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteUpdateInput>(args);
            var Result  = new NoteUpdateOutput();

            if (DbHolding.Notebook.IsExist(Input.NotebookName))
            {
                var notebook = DbHolding.Notebook.Get(Input.NotebookName);
                if (notebook.pass == Input.NotebookPass)
                {
                    if (notebook.note_list.Contains(Input.NoteId) && DbHolding.Note.IsExist(Input.NoteId))
                    {
                        var note    = DbHolding.Note.Get(Input.NoteId);
                        note.name   = Input.NewName;
                        note.type   = Input.NewType;
                        note.data   = Input.NewData;

                        Result.ResultStatus = ResultStatus.SuccessStatus;
                    }
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteMove(string args)
        {
            var Input = JsonConvert.DeserializeObject<NoteMoveInput>(args);
            var Result = new NoteMoveOutput();

            if (DbHolding.Notebook.IsExist(Input.FromName))
            {
                var FromNotebook = DbHolding.Notebook.Get(Input.FromName);
                if (FromNotebook.pass == Input.FromPass)
                {
                    if (FromNotebook.note_list.Contains(Input.NoteId) && DbHolding.Note.IsExist(Input.NoteId))
                    {
                        var note = DbHolding.Note.Get(Input.NoteId);
                        if (DbHolding.Notebook.IsExist(Input.ToName))
                        {
                            var ToNotebook = DbHolding.Notebook.Get(Input.ToName);
                            if(ToNotebook.pass == Input.ToPass)
                            {
                                ToNotebook.note_list.Add(note.id);
                                ToNotebook.notes_num++;

                                Result.ResultStatus = ResultStatus.SuccessStatus;
                            }
                        }
                    }
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteDelete(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteDeleteInput>(args);
            var Result  = new NoteDeleteOutput();

            if (DbHolding.Notebook.IsExist(Input.NotebookName))
            {
                var Notebook = DbHolding.Notebook.Get(Input.NotebookName);
                if (Notebook.pass == Input.NotebookPass)
                {
                    if (Notebook.note_list.Contains(Input.NoteId) && DbHolding.Note.IsExist(Input.NoteId))
                    {
                        Notebook.note_list.Remove(Input.NoteId);
                        Notebook.notes_num--;
                        DbHolding.Note.Delete(Input.NoteId);

                        Result.ResultStatus = ResultStatus.SuccessStatus;
                    }
                }
            }
            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}
