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

        private int toIDay(DateTime day)
        {
            return day.Year * 10000 + (day.Month + 1) * 100 + day.Day;
        }

        public void Init()
        {
            today = DateTime.Now;
            iday = toIDay(today);
            var numGood = random(iday, 98) % 3 + 2;
            var numBad = random(iday, 87) % 3 + 2;

            ActivityData activities = filter(data);
            var eventArr = pickRandomActivity(activities, numGood + numBad);
            Parse(eventArr);

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
            PickSpecials(eventArr);
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

        private List<string> pickRandom(List<string> array, int size)
        {
            List<string> result = new List<string>();
            for (var i = 0; i < array.Count; i++)
            {
                result.Add(array[i]);
            }
            for (var j = 0; j < array.Count - size; j++)
            {
                var index = random(iday, j) % result.Count;
                result.RemoveAt(index);
            }
            return result;
        }

        private void Parse(ActivityData activities)
        {
            for (int i = 0; i < activities.Data.Count; ++i)
            {
                var name = activities.Data[i].Name;
                if (name.IndexOf(@"%v") != -1)
                {
                    activities.Data[i].Name = name.Replace(@"%v", data.varNames[random(iday, 12) % data.varNames.Count]);
                }
                if (name.IndexOf(@"%t") != -1)
                {
                    activities.Data[i].Name = name.Replace(@"%t", data.tools[random(iday, 11) % data.tools.Count]);
                }
                if (name.IndexOf(@"%l") != -1)
                {
                    activities.Data[i].Name = name.Replace(@"%l", (random(iday, 12) % 247 + 30).ToString());
                }
            }
        }

        private void PickSpecials(ActivityData activities)
        {
            for (var i = 0; i < activities.specials.Count; i++)
            {
                var special = activities.specials[i];

                if (iday == toIDay(special.Date))
                {
                    ActivityDesc d = new ActivityDesc(special.Name, special.Desc);
                    if (special.Type == "good")
                    {
                        goodList.Add(d);
                    }
                    else
                    {
                        badList.Add(d);
                    }
                }
            }
        }

        public string GetDire()
        {
            return string.Format("座位朝向：面向{0} 写程序，BUG 最少。", data.directions[random(iday, 2) % data.directions.Count]);

        }

        public string GetDrink()
        {
            StringBuilder sb = new StringBuilder();
            var ds = pickRandom(data.drinks, 2);
            sb.Append("今日宜饮：");
            foreach (var obj in ds)
            {
                sb.Append(obj + " ");
            }
            return sb.ToString();
        }

        public string GetStar()
        {
            return Star(random(iday, 6) % 5 + 1);
        }

        private string Star(int num)
        {
            var result = "";
            var i = 0;
            while (i < num)
            {
                result += "★";
                i++;
            }
            while (i < 5)
            {
                result += "☆";
                i++;
            }
            return result;
        }
    }
}
