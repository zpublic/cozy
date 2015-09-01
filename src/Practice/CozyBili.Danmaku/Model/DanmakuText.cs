using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBili.Danmaku.Model
{
    class DanmakuText
    {
        public int Color { get; set; }
        public string Text { get; set; }
        public int TextSize { get; set; } = 40;
        public int Speed { get; set; } = 400;
    }
}
