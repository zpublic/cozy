using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Cache
{
    public class CacheManager
    {
        private static CacheManager instance { get; set; } = new CacheManager();
        public static CacheManager Instance { get { return instance; } }

        public PageCache HtmlCache { get; set; } = new PageCache();
        public PageCache MarkdownCache { get; set; } = new PageCache();

        private int timeout { get; set; }
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout                 = value;
                HtmlCache.Timeout       = value;
                MarkdownCache.Timeout   = value;
            }
        }

        private int maxSize { get; set; }
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                maxSize                 = value;
                HtmlCache.MaxSize       = value;
                MarkdownCache.MaxSize   = value;
            }
        }
    }
}
