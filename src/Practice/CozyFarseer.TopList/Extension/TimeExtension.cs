using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyFarseer.TopList.Extension
{
    public static class TimeExtension
    {
        public static string ToTotalTime(this int n)
        {
            var span = TimeSpan.FromMilliseconds(n);
            if (span.Days > 0)
            {
                return $"{span.Days}天前";
            }
            else if (span.Hours > 0)
            {
                return $"{span.Hours}小时前";
            }
            else if (span.Minutes > 0)
            {
                return $"{span.Minutes}分前";
            }
            else
            {
                return "刚才";
            }
        }
    }
}
