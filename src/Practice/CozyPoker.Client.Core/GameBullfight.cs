using CozyPoker.Engine.Model;
using CozyPoker.Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Client.Core
{
    // 斗牛游戏。每人发五张牌，按特定的计算方法比大小
    public class GameBullfight
    {
        CardCollect cc;

        public void Shuffle()
        {
            cc = NormalCardCollect.GetAPoker(false);
            cc.Shuffle();
        }

        public List<Card> Get()
        {
            return cc.Get(new SortedSet<int> { 0, 1, 2, 3, 4 });
        }

        // 比较两组牌的大小，不考虑一样大的情况
        public bool Compare(List<Card> a, List<Card> b)
        {
            int niuA = GetNiu(a);
            int niuB = GetNiu(b);
            if (niuA != niuB)
            {
                return niuA > niuB;
            }
            else
            {
                return CompareValue(a, b);
            }
        }

        // 只需要比最大的一张牌
        private bool CompareValue(List<Card> a, List<Card> b)
        {
            Card ma = GetMaxCard(a);
            Card mb = GetMaxCard(b);
            int cmp = NormalCardCompare.CompareNoJoker(ma, mb);
            if (cmp >= 0)
                return true;
            return false;
        }

        // 取最大的牌
        private Card GetMaxCard(List<Card> cs)
        {
            Card c = cs[0];
            foreach (var i in cs)
            {
                if (NormalCardCompare.CompareNoJoker(c, i) < 0)
                {
                    c = i;
                }
            }
            return c;
        }

        // 计算是牛几
        // 无牛（0） 牛1-9 牛牛（10） 五朵银花（11） 五朵金花（12）
        public int GetNiu(List<Card> cs)
        {
            int jinhua = 0;
            int yinhua = 0;
            int count = 0;
            foreach (var i in cs)
            {
                switch (i.Value)
                {
                    case CardValueType.Jack:
                    case CardValueType.Queen:
                    case CardValueType.King:
                        jinhua++;
                        break;
                    case CardValueType.C10:
                        yinhua++;
                        break;
                    default:
                        count += (byte)i.Value;
                        break;
                }
            }
            if (jinhua == 5) return 12;
            if (jinhua + yinhua == 5) return 11;
            if (count % 10 == 0) return 10;
            if (HasNiu(cs)) return count % 10;
            return 0;
        }

        // 判断有没有牛（只要有三张牌加起来是10的倍数就是有牛）
        public bool HasNiu(List<Card> cs)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = x + 1; y < 4; y++)
                {
                    for (int z = y + 1; z < 5; z++)
                    {
                        int count = GetValue(cs[x]) + GetValue(cs[y]) + GetValue(cs[z]);
                        if (count % 10 == 0)
                            return true;
                    }
                }
            }
            return false;
        }

        private int GetValue(Card c)
        {
            int v = (byte)c.Value;
            if (v > 9) v = 0;
            return v;
        }
    }
}
