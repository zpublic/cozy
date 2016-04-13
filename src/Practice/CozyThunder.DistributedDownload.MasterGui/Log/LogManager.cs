using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Log
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

        public void DownloadBeginLog()
        {
            AppendLog("开始下载");
        }

        public void DownloadEndLog()
        {
            AppendLog("下载完成");
        }

        public void ConnectLog(string addr)
        {
            AppendLog(string.Format("{0} 已连接", addr));
        }

        public void DisconnectLog()
        {
            AppendLog(string.Format("已断开"));
        }

        public void ScheduleTask(string addr, long from, long to)
        {
            AppendLog(string.Format("派发 [{0} , {1}] 到 {2}", from, to, addr));
        }

        public void ReceiveBegin(string url, long from, long to)
        {
            AppendLog(string.Format("开始传输 [{0} , {1}] {2}", from, to, url));
        }

        public void ReceiveEnd(string url, long from, long to)
        {
            AppendLog(string.Format("完成传输 [{0} , {1}] {2}", from, to, url));
        }
    }
}
