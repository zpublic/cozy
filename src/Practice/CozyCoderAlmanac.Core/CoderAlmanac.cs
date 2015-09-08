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
        DateTime today;
        int iday;
        List<ActivityDesc> goodList;
        List<ActivityDesc> badList;

        public void Init()
        {
            today = DateTime.Now;
            iday = today.Year * 10000 + (today.Month + 1) * 100 + today.Day;
            var numGood = random(iday, 98) % 3 + 2;
            var numBad = random(iday, 87) % 3 + 2;

            ActivityData activities = filter(data);
            var eventArr = pickRandomActivity(activities, numGood + numBad);
            goodList = new List<ActivityDesc>();
            for (var i = 0; i < numGood; i++)
            {
                ActivityDesc d = new ActivityDesc(eventArr.Data[i].Name, eventArr.Data[i].Good);
                goodList.Add(d);
            }
            badList = new List<ActivityDesc>();
            for (var i = 0; i < numBad; i++)
            {
                ActivityDesc d = new ActivityDesc(eventArr.Data[numGood + i].Name, eventArr.Data[numGood + i].Bad);
                badList.Add(d);
            }
        }

        public List<ActivityDesc> GetGoodActivity()
        {
            return goodList;
        }

        public List<ActivityDesc> GetBadActivity()
        {
            return badList;
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

        // 去掉一些不合今日的事件
        private ActivityData filter(ActivityData activities)
        {
            // 周末的话，只留下 weekend = true 的事件
            if (isWeekend())
            {
                ActivityData result = new ActivityData();
                result.Data = new List<Activity>();
                for (var i = 0; i < activities.Data.Count; i++)
                {
                    if (activities.Data[i].Weekend)
                    {
                        result.Data.Add(activities.Data[i]);
                    }
                }
                return result;
            }
            return activities;
        }

        private bool isWeekend()
        {
            return today.Day == 0 || today.Day == 6;
        }

        // 从 activities 中随机挑选 size 个
        private ActivityData pickRandomActivity(ActivityData activities, int size)
        {
            var picked_events = pickRandom(activities, size);
            return picked_events;
        }

        // 从数组中随机挑选 size 个
        private ActivityData pickRandom(ActivityData array, int size)
        {
            ActivityData result = new ActivityData();
            result.Data = new List<Activity>();
            for (var i = 0; i < array.Data.Count; i++)
            {
                result.Data.Add(array.Data[i]);
            }
            for (var j = 0; j < array.Data.Count - size; j++)
            {
                var index = random(iday, j) % result.Data.Count;
                result.Data.RemoveAt(index);
            }
            return result;
        }
    }
}
