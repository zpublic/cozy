using System;

namespace CozyBili.Core {

    public class DanMuModel {

        public string UserName { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }

        public static DanMuModel CreateModel(string jsonString) {
            //Todo
            return  new DanMuModel();
        }
    }
}