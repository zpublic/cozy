using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.SlaveGui.Log
{
    public class LogManager
    {
        public static LogManager Instalce { get; set; } = new LogManager();

        public event EventHandler<LogDataEventArgs> DataLogHander;
        public event EventHandler<LogClearEventArgs> ClearLogHander;

        private void AppendLog(string str)
        {
            DataLogHander(this, new LogDataEventArgs(str));
        }

        public void ClearLog()
        {
            ClearLogHander(this, new LogClearEventArgs());
        }

        public void TaskBeginLog()
        {
            AppendLog("开启服务");
        }

        public void TaskEndLog()
        {
            AppendLog("关闭服务");
        }

        public void ConnectLog(string addr)
        {
            AppendLog(string.Format("{0} 已连接", addr));
        }

        public void DisconnectLog()
        {
            AppendLog(string.Format("已断开"));
        }

        public void DownloadTaskBeginLog(string url, long from, long to)
        {
            AppendLog(string.Format("开始下载 [{0} , {1}] {2}", from, to, url));
        }

        public void DownloadTaskEndLog(string url, long from, long to)
        {
            AppendLog(string.Format("完成下载 [{0} , {1}] {2}", from, to, url));
        }

        public void TransferBegin(string url, long from, long to)
        {
            AppendLog(string.Format("开始传输 [{0} , {1}] {2}", from, to, url));
        }

        public void TransferEnd(string url, long from, long to)
        {
            AppendLog(string.Format("完成传输 [{0} , {1}] {2}", from, to, url));
        }
    }
}
