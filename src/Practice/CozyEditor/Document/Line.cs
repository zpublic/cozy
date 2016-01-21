using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Document
{
    public class Line
    {
        public string Content { get; set; }

        public string Source { get; set; }

        public int BeginPoint { get; set; }

        public DocumentCollection Documents { get; set; } = new DocumentCollection();
    }
}
