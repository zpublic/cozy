using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyNote.Model.APIModel;
using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using Newtonsoft.Json;
using CozyNote.ClientCore.Network;

namespace CozyNote.ClientCore.Api
{
    public static class NotebookApi
    {
        public static bool NotebookAll(string username, string userpass, ref List<int> notebooklist)
        {
            return UserApi.UserNotebook(username, userpass, ref notebooklist);
        }

        public static bool NotebookGet(int notebookid, string notebookpass, ref Tuple<string, int> info)
        {
            var input = new NotebookGetInput()
            {
                NotebookId  = notebookid,
                NotebookPass = notebookpass,
            };
            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.NotebookGet, json);

            var result      = JsonConvert.DeserializeObject<NotebookGetOutput>(output);
            var issuccess   = ResultStatus.IsSuccess(result.ResultStatus);
            if(issuccess)
            {
                info = Tuple.Create(result.NotebookName, result.NoteSum);
            }
            return issuccess;
        }

        public static bool NotebookList(int notebookid, string notebookpass, ref List<int> notelist)
        {
            var input = new NotebookListInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
            };
            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.NotebookList, json);

            var result      = JsonConvert.DeserializeObject<NotebookListOutput>(output);
            var issuccess   = ResultStatus.IsSuccess(result.ResultStatus);
            if (issuccess)
            {
                notelist = result.NoteList;
            }
            return issuccess;
        }

        public static bool NotebookUpdate(int notebookid, string notebookpass, string newname, string newpass)
        {
            var input = new NotebookUpdateInput()
            {
                NotebookId = notebookid,
                NotebookPass = notebookpass,
                NewName = newname,
                NewPass = newpass,
            };
            var json = JsonConvert.SerializeObject(input);
            var output = HttpReader.HttpPost(ApiDef.NotebookUpdate, json);

            var result = JsonConvert.DeserializeObject<NotebookUpdateOutput>(output);
            return ResultStatus.IsSuccess(result.ResultStatus);
        }

        public static bool NotebookCreate(string username, string userpass, string notebookname, string notebookpass, ref int id)
        {
            var input = new NotebookCreateInput()
            {
                UserName        = username,
                UserPass        = userpass,
                NotebookName    = notebookname,
                NotebookPass    = notebookpass,
            };
            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.NotebookCreate, json);

            var result      = JsonConvert.DeserializeObject<NotebookCreateOutput>(output);
            var issuccess   = ResultStatus.IsSuccess(result.ResultStatus);
            if (issuccess)
            {
                id = result.NotebookId;
            }
            return issuccess;
        }

        public static bool NotebookDelete(int notebookid, string notebookpass)
        {
            var input = new NotebookDeleteInput()
            {
                NotebookId      = notebookid,
                NotebookPass    = notebookpass,
            };
            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.NotebookDelete, json);

            var result = JsonConvert.DeserializeObject<NotebookDeleteOutput>(output);
            return ResultStatus.IsSuccess(result.ResultStatus);
        }
    }
}
