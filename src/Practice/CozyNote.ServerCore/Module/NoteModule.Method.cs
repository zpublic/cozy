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
            var Input       = JsonConvert.DeserializeObject<NoteCreateInput>(args);
            var Result      = new NoteCreateOutput();

            var notebook    = ModuleHelper.GetNotebook(Input.NotebookId, Input.NotebookPass);
            if(notebook != null)
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
                DbHolding.Notebook.Update(notebook);

                Result.NoteId       = id;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteGet(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteGetInput>(args);
            var Result  = new NoteGetOutput();

            var note    = ModuleHelper.GetNote(Input.NotebookId, Input.NotebookPass, Input.NoteId);
            if (note != null)
            {
                Result.Result       = note;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteUpdate(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteUpdateInput>(args);
            var Result  = new NoteUpdateOutput();

            var note    = ModuleHelper.GetNote(Input.NotebookId, Input.NotebookPass, Input.NoteId);
            if (note != null)
            {
                note.name           = Input.NewName;
                note.type           = Input.NewType;
                note.data           = Input.NewData;
                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteMove(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteMoveInput>(args);
            var Result  = new NoteMoveOutput();

            var FromNotebook    = ModuleHelper.GetNotebook(Input.FromId, Input.FromPass);
            var note            = ModuleHelper.GetNote(Input.FromId, Input.FromPass, Input.NoteId);

            if(note != null)
            {
                var ToNotebook = ModuleHelper.GetNotebook(Input.ToId, Input.ToPass);
                if (ToNotebook != null)
                {
                    ToNotebook.note_list.Add(note.id);

                    FromNotebook.note_list.Remove(note.id);

                    note.notebook_id = ToNotebook.id;

                    DbHolding.Note.Update(note);
                    DbHolding.Notebook.Update(FromNotebook);
                    DbHolding.Notebook.Update(ToNotebook);

                    Result.ResultStatus = ResultStatus.SuccessStatus;
                }
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }

        public string OnNoteDelete(string args)
        {
            var Input   = JsonConvert.DeserializeObject<NoteDeleteInput>(args);
            var Result  = new NoteDeleteOutput();

            var notebook    = ModuleHelper.GetNotebook(Input.NotebookId, Input.NotebookPass);
            var note        = ModuleHelper.GetNote(Input.NotebookId, Input.NotebookPass, Input.NoteId);

            if(note != null)
            {
                notebook.note_list.Remove(note.id);
                DbHolding.Note.Delete(note.id);

                DbHolding.Notebook.Update(notebook);

                Result.ResultStatus = ResultStatus.SuccessStatus;
            }

            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}
