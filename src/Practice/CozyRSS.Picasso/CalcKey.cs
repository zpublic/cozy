using CozyRSS.Syndication.Model;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CozyRSS.Picasso
{
    class CalcKey
    {
        Dictionary<string, string> _FeedKey;

        public CalcKey()
        {
            _FeedKey = new Dictionary<string, string>();
        }

        public string CalcFeedItemKey(SyndicationItem item)
        {
            if (item.pubDate != null)
                return item.pubDate;
            else if (item.title != null)
                return item.title;
            else
                return GetMd5Hash(item.description);
        }

        public string CalcFeedKey(string url)
        {
            lock(_FeedKey)
            {
                if (_FeedKey.ContainsKey(url))
                {
                    return _FeedKey[url];
                }
                else
                {
                    string key = GetMd5Hash(url);
                    _FeedKey[url] = key;
                    return key;
                }
            }
        }

        string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
