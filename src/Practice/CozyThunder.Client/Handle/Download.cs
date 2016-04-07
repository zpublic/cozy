using Common.Logger;
using CozyThunder.HttpDownload;
using CozyThunder.Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyThunder.Client
{
    public class Download
    {
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        public delegate void FinishDelegate();
        public event FinishDelegate Finished;
        public delegate void ErrorDelegate(string error);
        public event ErrorDelegate Errored;
        private string mUrl;
        private string mFilename;
        private DownloadTask mTask;

        public Download(string url, string filename)
        {
            this.mUrl = url;
            this.mFilename = filename;
            this.Init();
        }

        private void Init()
        {
            string dir = Path.GetDirectoryName(this.mFilename);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (File.Exists(this.mFilename))
            {
                File.Delete(this.mFilename);
            }
        }

        public void Start()
        {
            try
            {
                mLog.Info("Start download proccess.");
                Thread t = new Thread(Process);
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the start dowmload,error:{0}", e.ToString());
                throw;
            }
        }

        private void Process()
        {
            mLog.Info("Download process start,url:[{0}],filename;[{0}]", this.mUrl, this.mFilename);
            DownloadTask task = new DownloadTask();
            task.RemotePath = this.mUrl;
            task.LocalPath = this.mFilename;
            task.BlockSize = 1024 * 1024 * 3;
            if (task.InitTask())
            {
                this.ThreadDownload(task);
            }
        }

        void ThreadDownload(DownloadTask task)
        {
            this.mTask = task;
            Parallel.For(0, 3, (i) =>
            {
                DownloadThread();
            });
            mLog.Info("Download all finish.");
            this.Finished();
        }

        void DownloadThread()
        {
            try
            {
                mLog.Info("Download thread id:[{0}]", Thread.CurrentThread.ManagedThreadId.ToString());
                DownloadSubTask subTask = this.mTask.GetSubTask();
                while (subTask != null)
                {
                    byte[] data = HttpDownloadRange.Download(subTask.RemotePath, subTask.from, subTask.to);
                    if (data == null)
                    {
                        mLog.Error("An error has occurred in the dowmload thread,error:noknown.");
                        break;
                    }
                    subTask.data = data;
                    var b = this.mTask.UpdateTaskState(subTask, 2);
                    if (b)
                    {
                        mLog.Info("Thread Download finish.");
                        break;
                    }
                    subTask = this.mTask.GetSubTask();
                }
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the dowmload thread,error:{0}", e.ToString());
                this.Errored(e.Message);
            }
        }
    }
}
