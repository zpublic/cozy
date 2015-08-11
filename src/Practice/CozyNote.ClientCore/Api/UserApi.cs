using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using CozyNote.Model.APIModel;
using Newtonsoft.Json;
using CozyNote.ClientCore.Network;
using System.Collections.Generic;

namespace CozyNote.ClientCore.Api
{
    public static class UserApi
    {
        public static bool UserCreate(string username, string pass, ref int userid)
        {
            var input = new UserCreateInput()
            {
                UserName = username,
                UserPass = pass,
            };

            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.UserCreate, json);

            var result  = JsonConvert.DeserializeObject<UserCreateOutput>(output);
            var issuccess = ResultStatus.IsSuccess(result.ResultStatus);
            if (issuccess)
            {
                userid = result.UserId;
            }
            return issuccess;
        }

        public static bool  UserNotebook(string username, string userpass, ref List<int> notebooklist)
        {
             var input = new UserNotebookInput()
            {
                UserName    = username,
                UserPass    = userpass,
            };
            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.UserNotebook, json);

            var result  = JsonConvert.DeserializeObject<UserNotebookOutput>(output);
            var issuccess = ResultStatus.IsSuccess(result.ResultStatus);
            if (issuccess)
            {
                notebooklist = result.NotebookList;
            }
            return issuccess;
        }

        public static bool UserUpdate (string username, string userpass, string newname, string newpass)
        {
            var input = new UserUpdateInput()
            {
                UserName    = username,
                UserPass    = userpass,
                NewName     = newname,
                NewPass     = newpass,
            };
            var json    = JsonConvert.SerializeObject(input);
            var output  = HttpReader.HttpPost(ApiDef.UserUpdate, json);

            var result  = JsonConvert.DeserializeObject<UserUpdateOutput>(output);
            return ResultStatus.IsSuccess(result.ResultStatus);
        }
    }
}
