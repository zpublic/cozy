using CozyPoker.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Util
{
    public static class NolmalCardCollect
    {
        // A-K
        public static CardCollect Get13(CardSuiteType t)
        {
            CardCollect cc = new CardCollect();
            for (byte b = 1; b <= 13; ++b)
            {
                cc.Add(new Card((CardValueType)b, t));
            }
            return cc;
        }

        // A-K no 2
        public static CardCollect Get12(CardSuiteType t)
        {
            CardCollect cc = new CardCollect();
            for (byte b = 1; b <= 13; ++b)
            {
                if (b == 2) continue;
                cc.Add(new Card((CardValueType)b, t));
            }
            return cc;
        }

        // A-10
        public static CardCollect Get10(CardSuiteType t)
        {
            CardCollect cc = new CardCollect();
            for (byte b = 1; b <= 10; ++b)
            {
                cc.Add(new Card((CardValueType)b, t));
            }
            return cc;
        }

        public static CardCollect Get2Joker()
        {
            CardCollect cc = new CardCollect();
            cc.Add(new Card(CardValueType.BlackJoker, CardSuiteType.Joker));
            cc.Add(new Card(CardValueType.RedJoker, CardSuiteType.Joker));
            return cc;
        }

        public static CardCollect GetAPoker(bool bNeedJoker = true, bool bNeed2 = true)
        {
            CardCollect cc = new CardCollect();
            if (bNeedJoker)
            {
                cc.Add(Get2Joker().Cards);
            }
            if (bNeed2)
            {
                cc.Add(Get13(CardSuiteType.Clubs).Cards);
                cc.Add(Get13(CardSuiteType.Diamons).Cards);
                cc.Add(Get13(CardSuiteType.Hearts).Cards);
                cc.Add(Get13(CardSuiteType.Spades).Cards);
            }
            else
            {
                cc.Add(Get12(CardSuiteType.Clubs).Cards);
                cc.Add(Get12(CardSuiteType.Diamons).Cards);
                cc.Add(Get12(CardSuiteType.Hearts).Cards);
                cc.Add(Get12(CardSuiteType.Spades).Cards);
            }
            return cc;
        }
    }
}
