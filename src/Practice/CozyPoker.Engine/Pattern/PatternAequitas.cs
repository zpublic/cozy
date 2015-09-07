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
        public CardCollectMethod Step1 { get; set; }
        
        // 2，洗牌
        public ShuffleMethod Step2 { get; set; }

        // 3，发牌
        public DealMethod Step3 { get; set; }

        override public bool Init(string script)
        {
            if (base.Init(script))
            {
                Step1 = CardCollectMethodParser.ParseCardCollectMethod(mapMethod["s1"]);
                if (Step1 != null
                    && Step2 != null
                    && Step3 != null)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Card> Run()
        {
            var cc = Step1.Run();
            Step2.Run(cc);
            return Step3.Run(cc);
        }
    }
}
