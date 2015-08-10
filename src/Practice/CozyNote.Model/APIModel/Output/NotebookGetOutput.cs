using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Output
{
    public class NotebookGetOutput
    {
        public int ResultStatus { get; set; }

        public string NotebookName { get; set; }

        public int NoteSum { get; set; }
    }
}
