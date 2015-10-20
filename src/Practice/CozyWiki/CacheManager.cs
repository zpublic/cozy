using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki
{
    public class CacheManager
    {
        private static CacheManager instance { get; set; } = new CacheManager();
        public static CacheManager Instance { get { return instance; } }

        public PageCache MarkdownCache { get; set; } = new PageCache();
        public PageCache HtmlCache { get; set; } = new PageCache();
    }
}
