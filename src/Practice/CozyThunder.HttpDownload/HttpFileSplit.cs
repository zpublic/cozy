using System;
using System.Net;

namespace CozyThunder.HttpDownload
{
    public class HttpFileSplit
    {
        public string url_;
        public long blockSize_;
        public long fileSize_;
        public long blockCount_;
        public long lastBlockSize_;

        public static long TryGetContentLength(string url)
        {
            long len    = 0;
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    len = response.ContentLength;
                    request.Abort();
                }
            }
            catch(Exception)
            {
            }

            return len;
        }

        public bool TryToSplit(string url, long blockSize = 1024 * 3)
        {
            url_ = url;
            blockSize_ = blockSize;

            fileSize_ = TryGetContentLength(url);
            if (fileSize_ != 0)
            {
                blockCount_ = (fileSize_ + blockSize_ - 1) / blockSize_;
                lastBlockSize_ = fileSize_ % blockSize_;
                if (lastBlockSize_ == 0)
                    lastBlockSize_ = blockSize_;
                return true;
            }

            return false;
        }
    }
}
