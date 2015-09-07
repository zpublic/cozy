using CozyPoker.Engine.Method;
using CozyPoker.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Pattern
{
    // 发固定的牌，然后进行比大小（斗牛、扎金花、梭哈）
    public class PatternFirehawk : PatternBase
    {
        // 1，取牌
        private CardCollectMethod s1;

        // 2，洗牌
        private ShuffleMethod s2;

        // 3，发牌
        private DealMethod s3;

        // 4，算分方式
        private CalcCardsValueMethod s4;

        override public bool Init(string script)
        {
            if (base.Init(script))
            {
                s1 = CardCollectMethodParser.Parse(mapMethod["s1"]);
                s2 = ShuffleMethodParser.Parse(mapMethod["s2"]);
                s3 = DealMethodParser.Parse(mapMethod["s3"]);
                s4 = CalcCardsValueMethodParser.Parse(mapMethod["s4"]);
                if (s1 != null
                    && s2 != null
                    && s3 != null
                    && s4 != null)
                {
                    return true;
                }
            }
            return false;
        }

        private CardCollect cc_;

        // 模式是洗牌->发几组牌->比较大小
        public void Shuffle()
        {
            cc_ = s1.Run();
            s2.Run(cc_);
        }

        public CardCollect Deal()
        {
            return s3.Run(cc_);
        }

        public int Compare(CardCollect ccA, CardCollect ccB)
        {
            int a = s4.Run(ccA);
            int b = s4.Run(ccB);
            if (a > b)
                return 1;
            else if (b > a)
                return -1;
            return 0;
        }
    }
}
