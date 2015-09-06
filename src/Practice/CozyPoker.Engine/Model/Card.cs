using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Model
{
    public enum CardValueType : byte
    {
        Ace        = 1,
        C2         = 2,
        C3         = 3,
        C4         = 4,
        C5         = 5,
        C6         = 6,
        C7         = 7,
        C8         = 8,
        C9         = 9,
        C10        = 10,
        Jack       = 11,
        Queen      = 12,
        King       = 13,
        BlackJoker = 14,    // 小王
        RedJoker   = 15,    // 大王
    }

    public enum CardSuiteType : byte
    {
        Spades  = 1,    // 黑桃
        Clubs   = 2,    // 梅花
        Diamons = 3,    // 方块
        Hearts  = 4,    // 红桃
        Joker   = 5,    // 鬼（王牌）
    }

    public class Card
    {
        public Card(CardValueType v, CardSuiteType s)
        {
            Value = v;
            Suite = s;
        }

        public CardValueType Value { get; set; }

        public CardSuiteType Suite { get; set; }

        public string ToString(int type = 0)
        {
            string s = "";
            switch (Suite)
            {
                case CardSuiteType.Spades:
                    s += "黑桃";
                    break;
                case CardSuiteType.Clubs:
                    s += "梅花";
                    break;
                case CardSuiteType.Diamons:
                    s += "方块";
                    break;
                case CardSuiteType.Hearts:
                    s += "红桃";
                    break;
            }
            switch (Value)
            {
                case CardValueType.Ace:
                    s += "A";
                    break;
                case CardValueType.Jack:
                    s += "J";
                    break;
                case CardValueType.Queen:
                    s += "Q";
                    break;
                case CardValueType.King:
                    s += "K";
                    break;
                case CardValueType.BlackJoker:
                    s += "大王";
                    break;
                case CardValueType.RedJoker:
                    s += "小王";
                    break;
                default:
                    s += ((byte)Value).ToString();
                    break;
            }
            return s;
        }
    }
}
