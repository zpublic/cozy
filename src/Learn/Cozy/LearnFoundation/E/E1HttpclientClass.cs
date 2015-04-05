using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
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
        }
    }
}
