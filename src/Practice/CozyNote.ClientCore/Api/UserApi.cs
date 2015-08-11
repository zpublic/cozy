using CozyNote.Model.APIModel.Input;
using CozyNote.Model.APIModel.Output;
using Newtonsoft.Json;

namespace CozyNote.ClientCore.Api
{
    public class UserApi
    {
        public static int CreateUser(string username, string pass)
        {
            var usercreateinput = new UserCreateInput()
            {
                UserName = username,
                UserPass = pass,
            };

            var json = JsonConvert.SerializeObject(usercreateinput);

            var output = json;//usermodule.OnUserCreate(json);

            var usercreateoutput = JsonConvert.DeserializeObject<UserCreateOutput>(output);
            return usercreateoutput.UserId;
        }
    }
}
