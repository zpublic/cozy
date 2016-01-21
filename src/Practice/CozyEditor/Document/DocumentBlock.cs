using CozyEditor.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Document
{
    public class DocumentBlock
    {
        public DocumentTypeEnum DocumentType { get; set; }

        public Place StartPlace { get; set; }   = Place.Zero;

        public string Content { get; set; }

        public DocumentBlock Clone()
        {
            return new DocumentBlock()
            {
                Content         = this.Content,
                DocumentType    = this.DocumentType,
                StartPlace      = this.StartPlace,
            };
        }
    }
}
