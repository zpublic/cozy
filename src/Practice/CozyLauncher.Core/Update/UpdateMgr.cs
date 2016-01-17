using CozyLauncher.Infrastructure;
using CozyLauncher.Infrastructure.Http;
using System.Collections.Generic;

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
            local_ = GetLocalFileVersionInfo();
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
            if (HttpDownload.HttpDownloadFile(RemoteFileList, PathTransform.LocalFullPath("./a.list")))
            {

            }
            return null;
        }

        private List<FileVersionInfo> GetLocalFileVersionInfo()
        {
            return null;
        }
    }
}
