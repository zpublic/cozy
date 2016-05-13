using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyRSS.Syndication.Model {

    public class AtomItem {

        // 所有都是可选的，但是标题和描述必须有一个
        public string title { get; set; }
        public string summary { get; set; }
        public string id { get; set; }
        public string published { get; set; } // rfc 822格式
    }
}
