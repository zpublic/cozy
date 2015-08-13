using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ConsoleClient
{
    public class MenuItem
    {
        public virtual string Text { get; set; }

        public Action Command { get; set; }
    }
}
