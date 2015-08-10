using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.APIModel.Input
{
    public class NotebookUpdateInput
    {
        public int NotebookId { get; set; }

        public string NotebookPass { get; set; }

        public string NewName { get; set; }

        public string NewPass { get; set; }
    }
}
