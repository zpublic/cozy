using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NoteUpdateInput
    {
        public int NotebookId { get; set; }

        public string NotebookPass { get; set; }

        public int NoteId { get; set; }

        public string NewName { get; set; }

        public int NewType { get; set; }

        public string NewData { get; set; }
    }
}
