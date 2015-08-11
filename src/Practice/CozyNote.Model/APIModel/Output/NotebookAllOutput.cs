using CozyNote.Model.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Output
{
    public class NotebookAllOutput : OutputBase
    {
        public List<int> NotebookList { get; set; }
    }
}
