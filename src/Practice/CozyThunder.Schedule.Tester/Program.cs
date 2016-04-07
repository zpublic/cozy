using CozyThunder.HttpDownload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyThunder.Schedule.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadTask task = new DownloadTask();
            task.RemotePath = @"http://speed.myzone.cn/pc_elive_1.1.rar";
            task.LocalPath = @"d:\hehe.rar";
            task.BlockSize = 1024 * 1024 * 3;
            if (task.InitTask())
            {
                Console.WriteLine("begin download");
                Program p = new Program();
                p.ThreadDownload(task);
            }
            Console.ReadKey();
        }

        DownloadTask task_;
        void ThreadDownload(DownloadTask task)
        {
            task_ = task;
            for (int i = 0; i < 3; i++)
            {
                new Thread(new ThreadStart(Download)).Start();
            }
        }

        void Download()
        {
            DownloadSubTask subTask = task_.GetSubTask();
            while (subTask != null)
            {
                byte[] data = HttpDownloadRange.Download(subTask.RemotePath, subTask.from, subTask.to);
                if (data == null)
                {
                    Console.WriteLine("download error");
                    break;
                }
                subTask.data = data;
                var b = task_.UpdateTaskState(subTask, 2);
                if (b)
                {
                    Console.WriteLine("download finish");
                    break;
                }
                subTask = task_.GetSubTask();
            }
        }
    }
}
