using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NoteCreateInput
    {
        public string NotebookName { get; set; }

        public string NotebookPass { get; set; }

        public string NoteName { get; set; }

        public int NoteType { get; set; }

        public string NoteData { get; set; }
    }
}
