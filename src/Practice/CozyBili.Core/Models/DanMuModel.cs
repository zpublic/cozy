using System;
using Newtonsoft.Json.Linq;

namespace CozyBili.Core.Models
{

    public class DanMuModel
    {

        public string UserName { get; set; }
        public string Content { get; set; }

        public static DanMuModel CreateModel(string jsonString, int version = 1)
        {
            var reslut = JObject.Parse(jsonString);
            var model = new DanMuModel();
            switch (version)
            {
                case 1:
                    {
                        model.UserName = reslut["info"][2][1].ToString();
                        model.Content = reslut["info"][1].ToString();
                        break;
                    }
                case 2:
                    {
                        string cmd = reslut["cmd"].ToString();
                        if (cmd == "DANMU_MSG")
                        {
                            model.Content = reslut["info"][1].ToString();
                            model.UserName = reslut["info"][2][1].ToString();
                        }
                        break;
                    }
                default:
                    break;

            }
            return model;
        }
    }
}