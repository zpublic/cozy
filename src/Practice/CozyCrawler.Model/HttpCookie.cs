using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Model
{
    public class HttpCookie
    {
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookie(string url, string cookieName, StringBuilder cookieData, ref int size);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;
            int datasize = 256;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookie(uri.ToString(), null, cookieData, ref datasize))
            {
                if (datasize <= 0) return null;
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookie(uri.ToString(), null, cookieData, ref datasize)) return null;
            }

            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }

        public static void PrintCookies(CookieContainer cookies, Uri uri)
        {
            CookieCollection cc = cookies.GetCookies(uri);
            foreach (var cook in cc)
            {
                Console.WriteLine(cook);
            }
        }
    }
}
