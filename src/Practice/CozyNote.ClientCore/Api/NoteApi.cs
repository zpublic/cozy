using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using CozyNote.Model.APIModel;
using Newtonsoft.Json;
using CozyNote.ClientCore.Network;
using CozyNote.Model.ObjectModel;
using System;

namespace CozyNote.ClientCore.Api
{
    public static class NoteApi
    {
        public static bool NoteCreate(int notebookid, string notebookpass, string notename, int type, string notedate, ref int noteid)
        {
            var input = new NoteCreateInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
                NoteName        = notename,
                NoteType        = type,
                NoteData        = notedate,
            };
            var json    = JsonConvert.SerializeObject(input);

            string output = null;
            try
            {
                output = HttpReader.HttpPost(ApiDef.NoteCreate, json);
            }
            catch (AggregateException)
            {
                return false;
            }

            var result      = JsonConvert.DeserializeObject<NoteCreateOutput>(output);
            var issuccess   = ResultStatus.IsSuccess(result.ResultStatus);
            if (issuccess)
            {
                noteid = result.NoteId;
            }
            return issuccess;
        }

        public static bool NoteGet(int notebookid, string notebookpass, int noteid, ref Tuple<int, string, string> note)
        {
            var input = new NoteGetInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
                NoteId          = noteid,
            };
            var json    = JsonConvert.SerializeObject(input);

            string output = null;
            try
            {
                output = HttpReader.HttpPost(ApiDef.NoteGet, json);
            }
            catch (AggregateException)
            {
                return false;
            }

            var result      = JsonConvert.DeserializeObject<NoteGetOutput>(output);
            var issuccess   = ResultStatus.IsSuccess(result.ResultStatus);
            if (issuccess)
            {
                note = Tuple.Create(result.Result.type, result.Result.name, result.Result.data);
            }
            return issuccess;
        }

        public static bool NoteUpdate(int notebookid, string notebookpass, int noteid, string newname, int newtype, string newdate)
        {
            var input = new NoteUpdateInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
                NoteId          = noteid,
                NewName         = newname,
                NewType         = newtype,
                NewData         = newdate,
            };
            var json    = JsonConvert.SerializeObject(input);

            string output = null;
            try
            {
                output = HttpReader.HttpPost(ApiDef.NoteUpdate, json);
            }
            catch (AggregateException)
            {
                return false;
            }

            var result = JsonConvert.DeserializeObject<NoteUpdateOutput>(output);
            return ResultStatus.IsSuccess(result.ResultStatus);
        }

        public static bool NoteMove(int fromid, string frompass, int toid, string topass, int noteid)
        {
            var input = new NoteMoveInput()
            {
                FromId      = fromid,
                FromPass    = frompass,
                ToId        = toid,
                ToPass      = topass,
                NoteId      = noteid,
            };
            var json    = JsonConvert.SerializeObject(input);

            string output = null;
            try
            {
                output = HttpReader.HttpPost(ApiDef.NoteMove, json);
            }
            catch (AggregateException)
            {
                return false;
            }

            var result = JsonConvert.DeserializeObject<NoteMoveOutput>(output);
            return ResultStatus.IsSuccess(result.ResultStatus);
        }

        public static bool NoteDelete(int notebookid, string notebookpass, int noteid)
        {
            var input = new NoteDeleteInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
                NoteId          = noteid,
            };
            var json    = JsonConvert.SerializeObject(input);

            string output = null;
            try
            {
                output = HttpReader.HttpPost(ApiDef.NoteDelete, json);
            }
            catch (AggregateException)
            {
                return false;
            }

            var result = JsonConvert.DeserializeObject<NoteDeleteOutput>(output);
            return ResultStatus.IsSuccess(result.ResultStatus);
        }
    }
}
