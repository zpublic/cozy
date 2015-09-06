using CozyPoker.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Util
{
    public class NormalCardCompare
    {
        // 比较两张牌的大小
        // cmpA23
        // 0：大小关系为1<2<3
        // 1：大小关系为2<3<1
        // 2：大小关系为3<1<2
        public static int CompareNoJoker(
            Card a,
            Card b,
            bool bCmpSuite = true,
            int cmpA23 = 0)
        {
            int vA = (byte)a.Value;
            int vB = (byte)b.Value;
            if (vA != vB)
            {
                switch (cmpA23)
                {
                    case 0:
                        break;
                    case 1:
                        if (vA == 1) vA = 16;
                        if (vB == 1) vB = 16;
                        break;
                    case 2:
                        if (vA == 1) vA = 16;
                        if (vA == 1) vB = 16;
                        if (vA == 2) vA = 17;
                        if (vB == 2) vB = 17;
                        break;
                }
                return vA > vB ? 1 : -1;
            }
            if (bCmpSuite && a.Suite != b.Suite)
            {
                // 黑>红>梅>方
                return GetSuiteValue(a) > GetSuiteValue(b) ? 1 : -1;
            }
            return 0;
        }

        private static int GetSuiteValue(Card c)
        {
            switch (c.Suite)
            {
                case CardSuiteType.Diamons:
                    return 1;
                case CardSuiteType.Clubs:
                    return 2;
                case CardSuiteType.Hearts:
                    return 3;
                case CardSuiteType.Spades:
                    return 4;
                case CardSuiteType.Joker:
                    return 5;
            }
            return 0;
        }
    }
}
