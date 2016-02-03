using CozyLauncher.Core.Update;
using CozyLauncher.Infrastructure;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace CozyLauncher.Tool.UpdateFeedGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<FileVersionInfo> filelist = new List<FileVersionInfo>();
            foreach (var file in Directory.GetFiles(args[0]))
            {
                if(Path.GetExtension(file) == ".exe" || Path.GetExtension(file) == ".dll")
                {
                    filelist.Add(new FileVersionInfo
                    {
                        Name = Path.GetFileName(file),
                        Md5 = FileMd5.GetMD5HashFromFile(PathTransform.LocalFullPath(file)),
                    });
                }
            }

            // to json
            var filename = (args.Length < 2 || string.IsNullOrEmpty(args[1])) ? "./publish.json" : args[1];

            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var sr = new StreamWriter(fs))
                {
                    sr.Write(JsonConvert.SerializeObject(filelist));
                }
            }
        }
    }
}
