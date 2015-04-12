using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Cozy.LearnFoundation.E
{
    class E1HttpclientClass
    {
        static void GetData()
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage rsp = hc.GetAsync("http://apistore.baidu.com/microservice/iplookup?ip=113.106.106.131").Result;
            if (rsp.IsSuccessStatusCode)
            {
                string rspText = rsp.Content.ReadAsStringAsync().Result;
                Console.WriteLine(rspText);

                JsonReader jsonReader = new JsonTextReader(new StringReader(rspText));
                while (jsonReader.Read())
                {
                    if (jsonReader.Value != null)
                        Console.WriteLine("Token: {0}, Value: {1}", jsonReader.TokenType, jsonReader.Value);
                    else
                        Console.WriteLine("Token: {0}", jsonReader.TokenType);

                }

            }
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            GetData();
            Downloading_Files();
            Uploading_Files();
        }

        public static void Downloading_Files()
        {
            string url = @"https://www.baidu.com";

            WebClient wc = new WebClient();

            // 直接下载文件 
            //wc.DownloadFile(url, @"D:\index.html");

            Stream strm = wc.OpenRead(url);
            StreamReader sr = new StreamReader(strm);
            Console.WriteLine(sr.ReadLine());
        }

        public static void Uploading_Files()
        {
            //string url = @"http://www.baidu.com";
            //string fileName = @"D:\cozy.txt";

            //// 直接上传文件
            //WebClient wc = new WebClient();
            //wc.UploadFile(url, fileName);

            //// 上传数据
            //byte[] image = new byte[10];
            //wc.UploadData(url, image);
        }
    }
}
