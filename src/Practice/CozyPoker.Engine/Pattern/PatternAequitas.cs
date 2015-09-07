using CozyPoker.Engine.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Pattern
{
    // 发固定的牌，没有然后了（算24点、抽牌）
    public class PatternAequitas : PatternBase
    {
        // 1，取牌
        public CardCollectMethod Step1 { get; set; }
        
        // 2，洗牌
        public ShuffleMethod Step2 { get; set; }

        // 3，发牌
        public DealMethod Step3 { get; set; }
    }
}
