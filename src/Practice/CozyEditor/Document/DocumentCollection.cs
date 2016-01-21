using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Document
{
    public class DocumentCollection : IEnumerable<DocumentBlock>
    {
        public List<DocumentBlock> Contents { get; set; } = new List<DocumentBlock>();

        public int Count
        {
            get
            {
                return Contents.Count;
            }
        }

        public IEnumerator<DocumentBlock> GetEnumerator()
        {
            return Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Contents.GetEnumerator();
        }

        public void Add(DocumentBlock block)
        {
            Contents.Add(block);
        }
    }
}
