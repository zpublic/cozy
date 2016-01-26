using CozyLauncher.Infrastructure;
using CozyLauncher.Infrastructure.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace CozyLauncher.Core.Update
{
    /*
    升级流程
    1.0
    sync CheckUpdate
        下拉版本信息和文件版本list
        返回结果：需要更新/不需要更新
    sync GetFileVersionInfo
    外部下载替换
    */
    public class UpdateMgr
    {
        const string RemoteFileList = "http://www.laorouji.com/cozylauncher/a.list";

        List<FileVersionInfo> local_;
        List<FileVersionInfo> remote_;

        public bool CheckUpdate()
        {
            remote_ = GetRemoteFileVersionInfo();
            local_  = GetLocalFileVersionInfo();

            var fileDict = local_.ToDictionary(x => x.Name);
            foreach(var file in remote_)
            {
                if(!fileDict.ContainsKey(file.Name) || fileDict[file.Name].Md5 != file.Md5)
                {
                    return true;
                }
            }

            return false;
        }

        public List<FileVersionInfo> GetFileVersionInfo(bool bLocal = false)
        {
            if (bLocal)
            {
                return local_;
            }
            return remote_;
        }

        private List<FileVersionInfo> GetRemoteFileVersionInfo()
        {
            var data = HttpDownload.HttpGetString(RemoteFileList);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<List<FileVersionInfo>>(data);
            }
            return null;
        }

        private List<FileVersionInfo> GetLocalFileVersionInfo()
        {
            var filelist = new List<string> {
                "CozyLauncher.exe",
                "CozyLauncher.Core.dll",
                "CozyLauncher.Infrastructure.dll",
                "CozyLauncher.PluginBase.dll",

                "NHotkey.dll",
                "NHotkey.Wpf.dll",
                "Newtonsoft.Json.dll",
                "YAMP.dll",
                "System.Windows.Interactivity.dll",

                "CozyLauncher.Plugin.Core.dll",

                "CozyLauncher.Plugin.Program.dll",
                "CozyLauncher.Plugin.Dirctory.dll",
                "CozyLauncher.Plugin.ManualRun.dll",
                "CozyLauncher.Plugin.WebSearch.dll",
                "CozyLauncher.Plugin.Sys.dll",
                "CozyLauncher.Plugin.Calculator.dll",

                "CozyLauncher.Plugin.MouseClick.dll",
            };
            return filelist.Select(x => new FileVersionInfo { Name = x, Md5 = FileMd5.GetMD5HashFromFile(x) }).ToList();
        }
    }
}
