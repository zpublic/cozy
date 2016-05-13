using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyRSS.Syndication.Model {

    public class AtomFeed {

        public string title { get; set; }

        // 可选
        public string updated { get; set; }
        public List<AtomItem> entrys { get; set; }
    }
}
