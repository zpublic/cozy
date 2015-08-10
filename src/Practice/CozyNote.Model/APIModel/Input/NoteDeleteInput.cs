using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NoteDeleteInput
    {
        public string NotebookName { get; set; }

        public string NotebookPass { get; set; }

        public int NoteId { get; set; }
    }
}
