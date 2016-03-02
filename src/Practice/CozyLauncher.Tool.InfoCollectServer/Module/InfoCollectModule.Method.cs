using CozyLauncher.Tool.InfoCollectServer.Model;
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
            var Uuid    = Input.uuid;
            if(Uuid != null)
            {
                Console.WriteLine(Uuid);
                Result.ok = false;
            }
            var Output = JsonConvert.SerializeObject(Result);
            return Output;
        }
    }
}
