using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki
{
    public class Setting
    {
        public string RootDir { get; set; } = @".\";

        public int Port { get; set; } = 80;

        private static Setting instance { get; set; } = new Setting();
        public static Setting Instance
        {
            get
            {
                return instance;
            }
        }

        public void Init()
        {
            if (File.Exists("setting.json"))
            {
                using (var fs = new FileStream("setting.json", FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var context = reader.ReadToEnd();
                        instance = JsonConvert.DeserializeObject<Setting>(context);
                    }
                }
            }
            else
            {
                instance = new Setting();

                using (var fs = new FileStream("setting.json", FileMode.Create, FileAccess.ReadWrite))
                {
                    using (var writer = new StreamWriter(fs))
                    {
                        var context = JsonConvert.SerializeObject(instance);
                        writer.Write(context);
                    }
                }
            }
        }
    }
}
