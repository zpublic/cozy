using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ServerCore.Model
{
    public class Note
    {
        public int id { get; set; }
        public string name { get; set; }
        public string notebook_id { get; set; }
        public int type { get; set; }
        public string data { get; set; }
    }
}
