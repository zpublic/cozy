using CozyThunder.HttpDownload;
using System.Collections.Generic;
using System.IO;

namespace CozyThunder.Schedule
{
    public class DownloadTask
    {
        public string CfgPath;
        public string RemotePath;
        public string LocalPath;
        public long BlockSize = 1024 * 3;

        public string CacheFile { get { return cacheFile_; } }
        string cacheFile_;

        HttpFileSplit split_;
        Queue<DownloadSubTask> subTaskQueue_;
        byte[] taskState_; // 0-init 1-downloading 2-done 3-err
        int finishCount;
        object writeFile_ = new object();

        public bool InitTask()
        {
            split_ = new HttpFileSplit();
            if (split_.TryToSplit(RemotePath, BlockSize))
            {
                if (!CreateCacheFile()) return false;
                finishCount = 0;
                taskState_ = new byte[split_.blockCount_];
                subTaskQueue_ = new Queue<DownloadSubTask>();
                lock (subTaskQueue_)
                {
                    for (int i = 0; i < split_.blockCount_ - 1; ++i)
                    {
                        subTaskQueue_.Enqueue(new DownloadSubTask()
                        {
                            Id = i,
                            RemotePath = RemotePath,
                            from = split_.blockSize_ * i,
                            to = split_.blockSize_ * (i + 1) - 1,
                        });
                        taskState_[i] = 0;
                    }
                    subTaskQueue_.Enqueue(new DownloadSubTask()
                    {
                        Id = (int)split_.blockCount_ - 1,
                        RemotePath = RemotePath,
                        from = split_.blockSize_ * (split_.blockCount_ - 1),
                        to = split_.fileSize_ - 1,
                    });
                    taskState_[split_.blockCount_ - 1] = 0;
                }
                return true;
            }
            return false;
        }

        public DownloadSubTask GetSubTask()
        {
            lock (subTaskQueue_)
            {
                if (subTaskQueue_.Count > 0)
                {
                    return subTaskQueue_.Dequeue();
                }
            }
            return null;
        }

        public bool UpdateTaskState(DownloadSubTask task, byte state)
        {
            if (taskState_[task.Id] != state)
            {
                taskState_[task.Id] = state;
                if (state == 2)
                {
                    lock(writeFile_)
                    {
                        FileStream fsA = new FileStream(cacheFile_, FileMode.Open);
                        fsA.Seek(task.from, SeekOrigin.Begin);
                        fsA.Write(task.data, 0, task.data.Length);
                        fsA.Close();
                    }

                    finishCount++;
                    if (finishCount == split_.blockCount_)
                    {
                        lock (writeFile_)
                        {
                            FileInfo fi = new FileInfo(cacheFile_);
                            fi.MoveTo(LocalPath);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        bool CreateCacheFile()
        {
            cacheFile_ = LocalPath + ".cache";
            return CreateFixedSizeFile(cacheFile_, split_.fileSize_);
        }

        bool CreateFixedSizeFile(string fileName, long fileSize)
        {
            string dir = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                fs.SetLength(fileSize);
                if (fs != null)
                    fs.Close();
            }
            catch
            {
                if (fs != null)
                    fs.Close();
                return false;
            }
            return true;
        }
    }
}
