using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Event
{
    public class AddUrlEventArgs : EventArgsBase
    {
        public int Depth { get; set; }

        public override string Message
        {
            get
            {
                return base.Message + " " + Depth;
            }
        }

        public AddUrlEventArgs(string url, int depth)
            : base(url)
        {
            Depth = depth;
        }
    }
}
