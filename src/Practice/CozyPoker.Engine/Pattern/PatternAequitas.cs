using CozyPoker.Engine.Method;
using CozyPoker.Engine.Model;
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
        private CardCollectMethod s1;

        // 2，洗牌
        private ShuffleMethod s2;

        // 3，发牌
        private DealMethod s3;

        override public bool Init(string script)
        {
            if (base.Init(script))
            {
                s1 = CardCollectMethodParser.Parse(mapMethod["s1"]);
                s2 = ShuffleMethodParser.Parse(mapMethod["s2"]);
                s3 = DealMethodParser.Parse(mapMethod["s3"]);
                if (s1 != null
                    && s2 != null
                    && s3 != null)
                {
                    return true;
                }
            }
            return false;
        }

        public CardCollect Run()
        {
            var cc = s1.Run();
            s2.Run(cc);
            return s3.Run(cc);
        }
    }
}
