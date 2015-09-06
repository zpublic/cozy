using CozyPoker.Engine.Model;
using CozyPoker.Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Client.Core
{
    // 计算24的游戏，如果无解，答案的字符串为"no"
    public class Game24Calc
    {
        List<Card> cs;

        // 新开游戏，得到四张牌
        public List<Card> Get(bool bAllowNoAnswer = false)
        {
            do
            {
                CardCollect cc = NolmalCardCollect.GetAPoker(false);
                cc.Shuffle();
                cs = cc.Get(new SortedSet<int> { 0, 1, 2, 3 });
            } while (!bAllowNoAnswer && GetSolution() == "no");
            return cs;
        }

        // 校验计算表达式是否正确
        // -3:有解（表达式数字错误）
        // -2:有解（表达式结果错误）
        // -1:无解（回答错误）
        // 0:有解（回答正确）
        // 1:无解（回答正确）
        public int CheckAnswer(string expression)
        {
            return 0;
        }

        public string GetSolution()
        {
            return "//no";
        }
    }
}
