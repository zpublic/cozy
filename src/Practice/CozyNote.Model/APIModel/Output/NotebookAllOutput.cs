using CozyNote.Model.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Output
{
    public class NotebookAllOutput
    {
        public int ResultStatus { get; set; }

        public List<Notebook> NotebookList { get; set; }
    }
}
