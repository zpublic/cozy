using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.Model.ObjectModel;

namespace CozyNote.Model.APIModel.Output
{
    public class UserNotebookOutput
    {
        public int ResultStatus { get; set; }

        public List<int> NotebookList { get; set; }
    }
}
