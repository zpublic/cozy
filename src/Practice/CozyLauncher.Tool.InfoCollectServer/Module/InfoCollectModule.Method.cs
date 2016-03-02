using CozyLauncher.Shared.InfoCollect.Model;
using Newtonsoft.Json;
using System;

namespace CozyLauncher.Tool.InfoCollectServer.Module
{
    public partial class InfoCollectModule
    {
        public string OnInfocActive(string args)
        {
            var Input   = JsonConvert.DeserializeObject<InfocActiveInput>(args);
            var Result = new ResultOutput() { ok = false };
            var User    = Input.username;
            if(User != null)
            {
                Console.WriteLine(User);
                Result.ok = true;
            }
            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}
