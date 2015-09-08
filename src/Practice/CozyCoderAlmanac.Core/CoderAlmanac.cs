using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCoderAlmanac.Core
{
    public class CoderAlmanac
    {
        ActivityData data = new ActivityData();
        DateTime dt;
        List<ActivityDesc> goodList;
        List<ActivityDesc> badList;

        void Init()
        {
            DateTime today = DateTime.Now;
            var iday = today.Year * 10000 + (today.Month + 1) * 100 + today.Day;
            var numGood = random(iday, 98) % 3 + 2;
            var numBad = random(iday, 87) % 3 + 2;
        }

        List<ActivityDesc> GetGoodActivity()
        {
            return null;
        }

        List<ActivityDesc> GetBadActivity()
        {
            return null;
        }

        private int random(int dayseed, int indexseed)
        {
            var n = dayseed % 11117;
            for (var i = 0; i < 100 + indexseed; i++)
            {
                n = n * n;
                n = n % 11117;   // 11117 是个质数
            }
            return n;
        }
    }
}
