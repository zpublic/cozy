using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.HttpDownload.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://speed.myzone.cn/WindowsXP_SP2.exe";
            HttpFileSplit split = new HttpFileSplit();
            long blockSize = 1024 * 3;
            if (split.TryToSplit(url, blockSize))
            {
                Console.WriteLine("url:" + url);
                Console.WriteLine("fileSize_:" + split.fileSize_);
                Console.WriteLine("blockCount_:" + split.blockCount_);
                Console.WriteLine("lastBlockSize_:" + split.lastBlockSize_);

                Console.ReadKey();
                byte[] data = HttpDownloadRange.Download(url, 3 * split.blockSize_, 4 * split.blockSize_ - 1);
                if (data != null && data.Length == split.blockSize_)
                {
                    Console.WriteLine("HttpDownloadRange ok");
                }
            }
            Console.ReadKey();
        }
    }
}
