using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CozyWiki.Cache
{
    public class CacheManager
    {
        private static CacheManager instance { get; set; } = new CacheManager();
        public static CacheManager Instance { get { return instance; } }

        public PageCache HtmlCache { get; set; }            = new PageCache();
        public PageCache MarkdownCache { get; set; }        = new PageCache();

        Timer InnerTimer { get; set; } = new Timer();

        /// <summary>
        /// 缓存有效期
        /// </summary>
        public double Timeout
        {
            get
            {
                return InnerTimer.Interval;
            }
            set
            {
                InnerTimer.Interval = value;
            }
        }

        /// <summary>
        /// 缓存最大数量
        /// </summary>
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

        /// <summary>
        /// 是否开启定时自动清理无效缓存
        /// </summary>
        public bool CleanEnable
        {
            get
            {
                return InnerTimer.Enabled;
            }
            set
            {
                InnerTimer.Enabled = value;
            }
        }

        public CacheManager()
        {
            InnerTimer.Elapsed += new ElapsedEventHandler(OnTimerProcess);
        }

        private void OnTimerProcess(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            Predicate<CacheBlock> pre = x =>
            {
                if ((now - x.CacheTime).TotalMilliseconds > Timeout)
                {
                    return true;
                }
                return false;
            };

            HtmlCache.RemoveAll(pre);
            MarkdownCache.RemoveAll(pre);
        }
    }
}
