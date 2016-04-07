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

        public bool TryToSplit(string url, long blockSize = 1024 * 3)
        {
            url_ = url;
            blockSize_ = blockSize;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url_);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                fileSize_ = response.ContentLength;
                blockCount_ = (fileSize_ + blockSize_ - 1) / blockSize_;
                lastBlockSize_ = fileSize_ % blockSize_;
                if (lastBlockSize_ == 0)
                    lastBlockSize_ = blockSize_;
                request.Abort();
                response.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
