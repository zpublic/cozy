using System;
using Newtonsoft.Json.Linq;

namespace CozyBili.Core.Models {

    public class DanMuModel {

        public string UserName { get; set; }
        private int Milliseconds { get; set; }
        public string Content { get; set; }
        public DateTime Time {
            get {
                //别问我这里为什么乘于1000，返回的数据就这么坑,我也是参考别人的代码才知道的
                return ConvertTime(Milliseconds * 1000L);
            }
        }

        public static DanMuModel CreateModel(string jsonString) {
            var reslut = JObject.Parse(jsonString);
            var model = new DanMuModel {
                UserName = reslut["info"][2][1].ToString(),
                Content = reslut["info"][1].ToString(),
                Milliseconds = (int)reslut["info"][0][4]
            };
            return model;
        }

        private DateTime ConvertTime(long milliTime) {
            long timeTricks = new DateTime(1970, 1, 1).Ticks + milliTime * 10000 + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours * 3600 * (long)10000000;
            return new DateTime(timeTricks);
        }
    }
}