using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NotebookCreateInput
    {
        public string UserName { get; set; }

        public string UserPass { get; set; }

        public string NotebookName { get; set; }

        public string NotebookPass { get; set; }
    }
}
